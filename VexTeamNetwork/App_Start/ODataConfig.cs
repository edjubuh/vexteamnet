using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using VexTeamNetwork.Models;

namespace VexTeamNetwork
{
    public static class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            // Web API configuration and services
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.AddEnumType(typeof(Grade));
            builder.AddEnumType(typeof(Program));
            builder.EntitySet<Team>("Teams");
            //builder.EntitySet<Competition>("Competitions");

            config.MapODataServiceRoute(
                routePrefix: "odata",
                routeName: "odata",
                model: builder.GetEdmModel());
        }
    }
}