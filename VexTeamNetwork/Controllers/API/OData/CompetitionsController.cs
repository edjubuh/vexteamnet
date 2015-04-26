using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.OData;
using VexTeamNetwork.Models;

namespace VexTeamNetwork.Controllers.WebApi.OData
{
    public class CompetitionsController : ODataController
    {
        private NetworkContext db = new NetworkContext();

        // GET: odata/Competitions
        [EnableQuery, ResponseType(typeof(IQueryable<Competition>))]
        public IHttpActionResult Get()
        {
            return Ok(db.Competitions);
        }

        // GET: odata/Competitions(5)
        [EnableQuery, ResponseType(typeof(Competition))]
        public IHttpActionResult GetCompetition([FromODataUri] string key)
        {
            if (!CompetitionExists(key))
                return NotFound();
            return Ok(SingleResult.Create(db.Competitions.Where(c => c.Sku == key)));
        }

        [Authorize(Roles="Administrator")]
        [ResponseType(typeof(Competition))]
        public async Task<IHttpActionResult> Post(Competition comp)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            db.Competitions.Add(comp);
            await db.SaveChangesAsync();
            return Created(comp);
        }

        [Authorize(Roles="Administrator")]
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!CompetitionExists(key))
                return NotFound();
            db.Competitions.Remove(db.Competitions.Find(key));
            await db.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles="Administrator")]
        [ResponseType(typeof(Competition))]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Competition> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comp = await db.Teams.FindAsync(key);
            if (comp == null)
                return NotFound();
            try
            {
                await db.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!CompetitionExists(key))
                    return NotFound();
                else throw;
            }
            return Updated(comp);
        }

        [Authorize(Roles="Administrator")]
        [ResponseType(typeof(Competition))]
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Competition comp)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != comp.Sku)
                return BadRequest();
            db.Entry(comp).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                if (!CompetitionExists(key))
                    return NotFound();
                else throw;
            }
            return Updated(comp);
        }

        private bool CompetitionExists(string key)
        {
            return db.Competitions.Any(c => c.Sku == key);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
