using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.OData;
using System.Web.OData.Routing;
using VexTeamNetwork.Models;

namespace VexTeamNetwork.Controllers.WebApi.OData
{
    public class MatchesController : ODataController
    {
        private NetworkContext db = new NetworkContext();

        [EnableQuery, ResponseType(typeof(IQueryable<Match>))]
        public IHttpActionResult Get()
        {
            return Ok(
                (new List<Match>() { 
                    new Match() { Sku = "RE-VRC-15-0000", Round = Models.Round.Practice, DivisionName = "Division", Number = 47, Instance = 0 }
                }).AsQueryable());
        }

        [ODataRoute("Matches(Sku={Sku},Division={Division},Round={Round})")]
        [EnableQuery, ResponseType(typeof(IQueryable<Match>))]
        public IHttpActionResult GetMatch([FromODataUri] string Sku, [FromODataUri] string Division, [FromODataUri] string Round)
        {
            return Ok(
                (new List<Match>() { 
                    new Match() { Sku = "RE-VRC-15-0000", Round = Models.Round.Practice, DivisionName = "Division", Number = 47, Instance = 0 }
                }).AsQueryable());
            //if (!MatchExists(Sku, Division, Round))
            //    return NotFound();
            //return Ok(db.Matches.Where(m => m.Sku == Sku && m.DivisionName == Division && m.Round == Round));
        }

        private bool MatchExists(string sku, string div, Round round)
        {
            return db.Matches.Any(m => m.Sku == sku && m.DivisionName == div && m.Round == round);
        }
    }
}