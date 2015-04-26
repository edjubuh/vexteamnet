using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Linq;
using System;
using System.Web.OData;
using System.Web.OData.Routing;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.Routing;
using System.Net;

namespace VexTeamNetwork.Controllers.WebApi
{
    public class ApiControllerSelector : IHttpControllerSelector
    {
        private const string ApiKey = "api";
        private const string ControllerKey = "controller";

        private readonly HttpConfiguration config;
        private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> controllers;
        private readonly HashSet<string> duplicates;

        public ApiControllerSelector(HttpConfiguration configuration)
        {
            config = configuration;
            duplicates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(InitializeControllerDictionary);
        }

        private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
        {
            var dictionary = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);

            IAssembliesResolver assembliesResolver = config.Services.GetAssembliesResolver();
            IHttpControllerTypeResolver controllersResolver = config.Services.GetHttpControllerTypeResolver();

            ICollection<Type> controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);

            foreach (Type t in controllerTypes)
            {
                var segments = t.Namespace.Split(Type.Delimiter);

                var controllerName = t.Name.Remove(t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);

                if (segments[segments.Length - 1].Equals("vanilla", StringComparison.InvariantCultureIgnoreCase))
                    segments[segments.Length - 1] = "api";

                var key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", segments[segments.Length - 1], controllerName);

                if (dictionary.ContainsKey(key))
                    duplicates.Add(key);
                else
                    dictionary[key] = new HttpControllerDescriptor(config, t.Name, t);
            }

            foreach (string s in duplicates)
                dictionary.Remove(s);
            return dictionary;
        }

        private static T GetRouteVariable<T>(IHttpRouteData routeData, string name)
        {
            object result = null;
            if (routeData.Values.TryGetValue(name, out result))
                return (T)result;
            return default(T);
        }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return controllers.Value;
        }

        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            IHttpRouteData routeData = request.GetRouteData();
            if (routeData == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            string apiName = request.RequestUri.Segments[1].Replace("/", "");
            if (apiName == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            string controllerName = GetRouteVariable<string>(routeData, ControllerKey);
            if (controllerName == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            string key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", apiName, controllerName.ToString());

            HttpControllerDescriptor controllerDescriptor;
            if (controllers.Value.TryGetValue(key, out controllerDescriptor))
                return controllerDescriptor;
            else if (duplicates.Contains(key))
                throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Multiple controllers were found that match this request"));
            else
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}