using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.OData;
using System.Web.OData.Routing;
using VexTeamNetwork.Models;

namespace VexTeamNetwork.Controllers.WebApi.OData
{
    public class DivisionsController : ODataController
    {
        private NetworkContext db = new NetworkContext();

        [ODataRoute("Divisions(Sku={Sku})")]
        [EnableQuery, ResponseType(typeof(IQueryable<Division>))]
        public IHttpActionResult GetDivision([FromODataUri] string Sku)
        {
            if (!CompetitionExists(Sku))
                return NotFound();
            return Ok(db.Divisions.Where(div => div.Sku == Sku));
        }

        [ODataRoute("Divisions(Sku={Sku},Name={Name})")]
        [EnableQuery, ResponseType(typeof(Division))]
        public IHttpActionResult GetDivision([FromODataUri] string Sku, [FromODataUri]string Name)
        {
            if (!CompetitionExists(Sku) || !DivisionExists(Sku, Name))
                return NotFound();

            return Ok(SingleResult.Create(db.Divisions.Where(d => d.Sku == Sku && d.Name == Name)));
        }

        [ODataRoute("Divisions(Sku={Sku},Name={Name})/Matches")]
        [EnableQuery, ResponseType(typeof(IQueryable<Match>))]
        public IHttpActionResult GetMatches([FromODataUri] string Sku, [FromODataUri] string Name)
        {
            if (!CompetitionExists(Sku) || !DivisionExists(Sku, Name))
                return NotFound();

            return Ok(db.Matches.Where(match => match.Sku == Sku && match.DivisionName == Name));
        }

        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(Competition))]
        public async Task<IHttpActionResult> Post(Division comp)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            db.Divisions.Add(comp);
            await db.SaveChangesAsync();
            return Created(comp);
        }

        [Authorize(Roles = "Administrator")]
        [ODataRoute("Divisions(Sku={key},Name={name})")]
        public async Task<IHttpActionResult> Delete([FromODataUri] string key, [FromODataUri] string name)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!CompetitionExists(key))
                return NotFound();
            db.Divisions.Remove(db.Divisions.Find(key));
            await db.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        [ODataRoute("Divisions(Sku={key},Name={name})")]
        [ResponseType(typeof(Competition))]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, [FromODataUri] string name, Delta<Division> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var div = await db.Divisions.FindAsync(key);
            if (div == null)
                return NotFound();
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DivisionExists(key, name))
                    return NotFound();
                else throw;
            }
            return Updated(div);
        }

        [Authorize(Roles = "Administrator")]
        [ODataRoute("Divisions(Sku={key},Name={name})")]
        [ResponseType(typeof(Division))]
        public async Task<IHttpActionResult> Put([FromODataUri] string key, [FromODataUri] string name, Division division)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != division.Sku)
                return BadRequest();
            db.Entry(division).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                if (!DivisionExists(key, name))
                    return NotFound();
                else throw;
            }
            return Updated(division);
        }

        private bool CompetitionExists(string sku)
        {
            return db.Competitions.Any(c => c.Sku == sku);
        }

        private bool DivisionExists(string sku, string name)
        {
            return db.Divisions.Any(d => d.Name == name && d.Sku == sku);
        }
    }
}