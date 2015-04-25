using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.OData;
using VexTeamNetwork.Models;

namespace VexTeamNetwork.Controllers.WebApi.Vanilla
{
    [RoutePrefix("Teams")]
    public class TeamsController : ApiController
    {
        NetworkContext db = new NetworkContext();

        // GET: api/Teams
        [ResponseType(typeof(IQueryable<Team>))]
        [EnableQuery]
        public IHttpActionResult GetTeams()
        {
            return Ok(db.Teams);
        }


        // GET: api/Teams/1
        [ResponseType(typeof(Team))]
        public IHttpActionResult GetByNumber([FromUri] string id)
        {
            if (!TeamExists(id))
                return NotFound();
            return Ok(db.Teams.First(t => t.Number == id));
        }

        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(Team))]
        public async Task<IHttpActionResult> Post([FromBody]Team team)
        {
            if (TeamExists(team.Number))
                return BadRequest("Team already exists.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            db.Teams.Add(team);
            await db.SaveChangesAsync();
            return Created(Url.Route("DefaultApi", new { controller = "Teams", id = team.Number }), team);
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
