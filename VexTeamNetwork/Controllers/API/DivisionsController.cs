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
    public partial class DivisionsController : ODataController
    {
        NetworkContext db = new NetworkContext();

        [ODataRoute("Divisions(Sku={Sku},Name={Name})")]
        [EnableQuery, ResponseType(typeof(Division))]
        public IHttpActionResult GetDivision([FromODataUri] string Sku, [FromODataUri]string Name)
        {
            if (!CompetitionExists(Sku) || !DivisionExists(Sku, Name))
                return NotFound();

            return Ok(SingleResult.Create(db.Divisions.Where(d => d.Sku == Sku && d.Name == Name)));
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