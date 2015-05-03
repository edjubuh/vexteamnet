using System.Runtime.Serialization;
using System.Web.Http;
using Newtonsoft.Json;
using VexTeamNetwork.Models;

namespace VexTeamNetwork
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.XmlFormatter.SetSerializer<Division>(new DataContractSerializer(typeof(Division), null, int.MaxValue, false, true, null));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "WebApiDefault",
                "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                "DivisionsRoute",
                "api/{controller}/{sku}/{action}/{name}",
                defaults: new { name = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                "MatchesRoute",
                "api/Competitions/{sku}/Divisions/{div}/{controller}/{round}/{instance}/{number}",
                defaults: new { round = RouteParameter.Optional, instance = RouteParameter.Optional, number = RouteParameter.Optional });
        }
    }
}