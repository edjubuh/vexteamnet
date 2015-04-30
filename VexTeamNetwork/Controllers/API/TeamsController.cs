using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.OData;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Formatter;
using System.Web.OData.Formatter.Serialization;
using System.Web.OData.Query;
using System.Xml.Linq;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;
using Microsoft.Spatial;
using VexTeamNetwork.Models;

namespace VexTeamNetwork.Controllers.WebApi.OData
{
    public class TeamsController : ODataController
    {
        NetworkContext db = new NetworkContext();

        //[HttpGet]
        [EnableQuery(AllowedArithmeticOperators = AllowedArithmeticOperators.None,
            AllowedLogicalOperators = AllowedLogicalOperators.All,
            AllowedQueryOptions = AllowedQueryOptions.All,
            EnsureStableOrdering = true)]
        public IQueryable<Team> Get()
        {
            return db.Teams;
        }

        //[HttpGet]
        [EnableQuery]
        public SingleResult<Team> Get([FromODataUri]string key)
        {
            IQueryable<Team> result = db.Teams.Where(t => t.Number == key);
            return SingleResult.Create(result);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IHttpActionResult> Post(Team team)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            db.Teams.Add(team);
            await db.SaveChangesAsync();
            return Created(team);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IHttpActionResult> Delete([FromODataUri]string key)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!TeamExists(key))
                return NotFound();
            db.Teams.Remove(db.Teams.First(t => t.Number == key));
            await db.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IHttpActionResult> Patch([FromODataUri]string key, Delta<Team> delta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var team = await db.Teams.FindAsync(key);
            if (team == null)
                return NotFound();
            delta.Patch(team);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(key))
                    return NotFound();
                else throw;
            }
            return Updated(team);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Team team)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != team.Number)
                return BadRequest();
            db.Entry(team).State = System.Data.Entity.EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                if (!TeamExists(key))
                    return NotFound();
                else throw;
            }
            return Updated(team);
        }

        private bool TeamExists(string number)
        {
            return db.Teams.Any(t => t.Number == number);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
