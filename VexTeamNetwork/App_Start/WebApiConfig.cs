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
            builder.EntitySet<Team>("Teams");
            builder.EntitySet<Competition>("Competitions");
            builder.EntitySet<Division>("Divisions");
            var EdmModel = builder.GetEdmModel();

            //var divisions = (EdmEntitySet)EdmModel.EntityContainer.FindEntitySet("Divisions");
            //var divisionType = (EdmEntityType)EdmModel.FindDeclaredType("VexTeamNetwork.Models.Division");
            //var divisionProperty = new EdmNavigationPropertyInfo();
            //divisionProperty.TargetMultiplicity = EdmMultiplicity.Many;
            //divisionProperty.Target = (EdmEntityType)EdmModel.FindDeclaredType("VexTeamNetwork.Models.Competition");
            //divisionProperty.ContainsTarget = false;
            //divisionProperty.OnDelete = EdmOnDeleteAction.None;
            //divisionProperty.Name = "Divisions";

            //divisions.AddNavigationTarget(divisionType.AddUnidirectionalNavigation(divisionProperty), divisions);

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