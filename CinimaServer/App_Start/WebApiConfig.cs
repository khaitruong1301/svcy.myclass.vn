using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using WebApiContrib.Formatting.Jsonp;
using System.Web;
using CinimaServer.Areas.HelpPage;

namespace CinimaServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.EnableCors(new EnableCorsAttribute("*", headers: "*", methods: "*"));
            //Thêm description
            // config.SetDocumentationProvider(new XmlDocumentationProvider(
            //  HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));
            //support delete 23/1:2018

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HoHttpConfiguration config = GlobalConfiguration.Configuration;



            //// Web API routes
            //config.MapHttpAttributeRoutes();
            //config.EnableCors();

            //// Web API configuration and services
            //config.EnableCors();
            //// Web API routes
            //config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            new DefaultContractResolver { IgnoreSerializableAttribute = true };

            config.SuppressDefaultHostAuthentication();
            var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            config.Formatters.Add(jsonpFormatter);
            //Thêm vào 23/1/2018 http delete
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                 name: "ActionApi",
                 routeTemplate: "api/{controller}/{action}/{id}",
                 defaults: new { id = RouteParameter.Optional }
             );
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //var jf = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0, jf);
            //var jf = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0, jf);
        }
    }
}
