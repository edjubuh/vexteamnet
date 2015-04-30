using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Routing;
using System.Web.OData.Formatter;
using Newtonsoft.Json.Serialization;
using VexTeamNetwork.Models;
using System.Net.Http.Formatting;
using VexTeamNetwork.Controllers.API;

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
            var formatters = ODataMediaTypeFormatters.Create();
            config.Formatters.Clear();
            config.Formatters.InsertRange(0, formatters);
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //config.EnableCaseInsensitive(true);
            /*
            var traceWriter = config.EnableSystemDiagnosticsTracing();
            traceWriter.IsVerbose = true;
            traceWriter.MinimumLevel = System.Web.Http.Tracing.TraceLevel.Debug;
            */
            config.MapODataServiceRoute(
                routePrefix: "odata",
                routeName: "odata",
                model: builder.GetEdmModel());
        }
    }
}