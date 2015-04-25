using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.OData.Extensions;
using System.Web.OData.Formatter;
using Newtonsoft.Json.Serialization;
using VexTeamNetwork.Controllers.WebApi;

namespace VexTeamNetwork
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.AddODataQueryFilter();
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.Add(new XmlMediaTypeFormatter());
            config.Formatters.Add(new FormUrlEncodedMediaTypeFormatter());
            config.Formatters.InsertRange(3, ODataMediaTypeFormatters.Create());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.EnableCaseInsensitive(true);
            
            // Web API routes
            config.Routes.MapHttpRoute(
                name: "DefaultApiGetById",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id =  RouteParameter.Optional },
                constraints: new { id = "\\d+"}
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}");

            config.Services.Replace(typeof(IHttpControllerSelector), new ApiControllerSelector(config));
        }
    }
}
