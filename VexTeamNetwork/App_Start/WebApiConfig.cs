using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Routing;
using System.Web.OData.Formatter;
using Newtonsoft.Json.Serialization;
using VexTeamNetwork.Models;

namespace VexTeamNetwork
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            // Web API configuration and services
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.AddEnumType(typeof(Grade));
            builder.AddEnumType(typeof(Program));
            builder.EntitySet<Team>("Teams");
            builder.EntitySet<Competition>("Competitions");

            // Web API configuration and services
            var ODataFormatters = ODataMediaTypeFormatters.Create();
            config.Formatters.InsertRange(0, ODataFormatters);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.EnableCaseInsensitive(true);

            
            var route = config.MapODataServiceRoute("ODataRoute", "odata", builder.GetEdmModel());

            config.MapODataServiceRoute(
                routePrefix: "odata",
                routeName: "odata",
                model: builder.GetEdmModel());
        }
    }
}