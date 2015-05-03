using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.OData;
using VexTeamNetwork.Models;

namespace VexTeamNetwork.Controllers.API
{
    public class TeamsController : ApiController
    {
        private NetworkContext db = new NetworkContext();

        // GET: api/Teams
        [EnableQuery]
        public IQueryable<Team> GetTeams()
        {
            return db.Teams;
        }

        // GET: api/Teams/5
        [ResponseType(typeof(Team))]
        public IHttpActionResult GetTeam(string id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        // PUT: api/Teams/5
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeam(string id, Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.Number)
            {
                return BadRequest();
            }

            db.Entry(team).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // POST: api/Teams
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(Team))]
        public IHttpActionResult PostTeam(Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teams.Add(team);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TeamExists(team.Number))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = team.Number }, team);
        }

        // DELETE: api/Teams/5
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(Team))]
        public IHttpActionResult DeleteTeam(string id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            db.Teams.Remove(team);
            db.SaveChanges();

            return Ok(team);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(string id)
        {
            return db.Teams.Count(e => e.Number == id) > 0;
        }
    }
}