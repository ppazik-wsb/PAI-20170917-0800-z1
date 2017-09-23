﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HelloWorld
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "TestAuth",
                url: "Test/TestAuth/{poufne}",
                defaults: new { controller = "Test", action = "TestAuth", poufne = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "TestIndex",
                url: "Test",
                defaults: new { controller = "Test", action = "IndexView"}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
