using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Routing;
using System.Web.OData.Formatter;
using Newtonsoft.Json.Serialization;
using VexTeamNetwork.Models;
using System.Net.Http.Formatting;
using VexTeamNetwork.Controllers;
using System.Web.OData.Routing.Conventions;
using VexTeamNetwork.Controllers.WebApi.OData;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm;

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
            builder.AddEnumType(typeof(Round));
            builder.EntitySet<Team>("Teams");
            builder.EntitySet<Competition>("Competitions");
            builder.EntitySet<Division>("Divisions");
            builder.EntitySet<Match>("Matches");
            var EdmModel = builder.GetEdmModel();

            //builder.EnableLowerCamelCase();

            // Web API configuration and services
            var formatters = ODataMediaTypeFormatters.Create();
            config.Formatters.Clear();
            config.Formatters.InsertRange(0, formatters);

            var conventions = ODataRoutingConventions.CreateDefault();
            conventions.Insert(0, new CompositeKeyRoutingConvention());
            
            var traceWriter = config.EnableSystemDiagnosticsTracing();
            //traceWriter.IsVerbose = true;
            traceWriter.MinimumLevel = System.Web.Http.Tracing.TraceLevel.Debug;

            config.MapODataServiceRoute(
                routePrefix: "odata",
                routeName: "odata",
                model: EdmModel,
                pathHandler: new DefaultODataPathHandler(),
                routingConventions: conventions);
        }
    }
}