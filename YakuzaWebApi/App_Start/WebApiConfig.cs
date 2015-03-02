using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace YakuzaWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
           name: "PostEvent",
           routeTemplate: "api/{controller}/{action}/{gamesessionid}/{textcontent}/{date}/{isonlyformafia}",
           defaults: new { }
         );
            config.Routes.MapHttpRoute(
              name: "Join",
              routeTemplate: "api/{controller}/{action}/{username}/{sessionid}",
              defaults: new { username = RouteParameter.Optional, sessionid = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "GetSession",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "CreateSession",
                routeTemplate: "api/{controller}/{action}/{username}",
                defaults: new { username = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
             name: "DefaultGet",
             routeTemplate: "api/{controller}/{action}",
             defaults: new { }
           );
        }
    }
}
