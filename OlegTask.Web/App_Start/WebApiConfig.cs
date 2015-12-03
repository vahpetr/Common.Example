using System.Web.Http;
using System.Web.Http.ValueProviders;
using Common.MVC.Providers;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OlegTask.Web.Attributes;

namespace OlegTask.Web
{
    public static class WebApiConfig
    {
        public static JsonSerializerSettings JsonSerializerSettings { get; set; }

        public static void Register(HttpConfiguration config)
        {
            //// Web API configuration and services
            //// Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();

            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new ExceptionInterceptorAttribute());

            config.Services.Add(typeof (ValueProviderFactory), new EmptyValueProviderFactory());

            // Web API routes
            config.MapHttpAttributeRoutes(new InheritDirectRouteProvider());

            var formatters = config.Formatters;

            config.Formatters.Remove(formatters.XmlFormatter);

            var jsonFormatter = formatters.JsonFormatter;
            JsonSerializerSettings = jsonFormatter.SerializerSettings;
            JsonSerializerSettings.Formatting = Formatting.Indented;
            JsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            JsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional});
        }
    }
}