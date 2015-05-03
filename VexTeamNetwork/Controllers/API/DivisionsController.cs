using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.OData;
using VexTeamNetwork.Models;

namespace VexTeamNetwork.Controllers.API
{
    public partial class CompetitionsController : ApiController
    {
        // GET: api/Competitions/RE-VRC-00-0000/Divisions
        [EnableQuery, HttpGet, ActionName("Divisions")]
        public IQueryable<Division> GetDivisions(string sku)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var divs = db.Divisions.Where(div => div.Sku == sku);
            return divs;
        }

        // GET: api/Competitions/RE-VRC-00-0000/Divisions/5
        [ResponseType(typeof(Division))]
        [HttpGet, ActionName("Divisions")]
        public IHttpActionResult GetDivision(string sku, string name)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Division division = db.Divisions.Find(sku, name);
            if (division == null)
            {
                return NotFound();
            }

            return Ok(division);
        }

        // PUT: api/Competitions/RE-VRC-00-0000/Divisions/5
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(void))]
        [HttpPut, ActionName("Divisions")]
        public IHttpActionResult PutDivision(string sku, string name, Division division)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (sku != division.Sku)
            {
                return BadRequest();
            }

            db.Entry(division).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DivisionExists(sku, name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Competitions/RE-VRC-00-0000/Divisions
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(Division))]
        [HttpPost, ActionName("Divisions")]
        public IHttpActionResult PostDivision(Division division)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Divisions.Add(division);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DivisionExists(division.Sku, division.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = division.Sku }, division);
        }

        // DELETE: api/Competitions/RE-VRC-00-0000/Divisions/5
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(Division))]
        [HttpDelete, ActionName("Divisions")]
        public IHttpActionResult DeleteDivision(string id)
        {
            Division division = db.Divisions.Find(id);
            if (division == null)
            {
                return NotFound();
            }

            db.Divisions.Remove(division);
            db.SaveChanges();

            return Ok(division);
        }

        private bool DivisionExists(string sku, string name)
        {
            return db.Divisions.Count(e => e.Sku == sku && e.Name == name) > 0;
        }
    }
}