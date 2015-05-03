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
    public class MatchesController : ApiController
    {
        private NetworkContext db = new NetworkContext();

        // GET: api/Matches
        [EnableQuery]
        public IQueryable<Match> GetMatches()
        {
            return db.Matches;
        }

        // GET: api/Competitions/RE-VRC-00-0000/Divisons/MyDivision/Matches/Qualification/1/5
        [EnableQuery]
        public IQueryable<Match> GetMatches(string sku, string div, Round round = Round.Other, int? instance = null, int? number = null)
        {
            // Qualifications don't normally have an instance identifier, so ../../Qualification/4 maps to the fourth qualification match
            if (round == Round.Qualification && instance.HasValue && !number.HasValue)
            {
                return db.Matches.Where(m =>
                    m.Sku == sku &&
                    m.DivisionName == div &&
                    m.Round == round &&
                    m.Number == instance);
            }

            return db.Matches.Where(m =>
                m.Sku == sku &&
                m.DivisionName == div &&
                (round == Round.Other ? true : m.Round == round) &&
                (instance.HasValue ? true : m.Instance == instance.Value) &&
                (number.HasValue ? true : m.Number == number.Value));
        }

        // PUT: api/Matches/5
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMatch(string sku, string div, Round round, int instance, int number, Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (sku != match.Sku || div != match.DivisionName || round != match.Round || instance != match.Instance || number != match.Number)
            {
                return BadRequest();
            }

            db.Entry(match).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(sku, div, round, instance, number))
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

        // POST: api/Matches
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(Match))]
        public IHttpActionResult PostMatch(Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Matches.Add(match);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MatchExists(match.Sku, match.DivisionName, match.Round, match.Instance, match.Number))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = match.Sku }, match);
        }

        // DELETE: api/Matches/5
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(Match))]
        public IHttpActionResult DeleteMatch(string id)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }

            db.Matches.Remove(match);
            db.SaveChanges();

            return Ok(match);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatchExists(string sku, string div, Round round, int instance, int number)
        {
            return db.Matches.Count(e => e.Sku == sku && e.DivisionName == div && e.Round == round  && e.Instance == instance && e.Number == number) > 0;
        }
    }
}