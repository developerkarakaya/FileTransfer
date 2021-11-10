using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FileTransfer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "dosyalistesi", url: "dosyalistesi", defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute(name: "dosyagonder", url: "dosyagonder", defaults: new { controller = "Home", action = "files", id = UrlParameter.Optional });
            routes.MapRoute(name: "giris", url: "giris", defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional });
            routes.MapRoute(name: "cikis", url: "cikis", defaults: new { controller = "Account", action = "SignOut", id = UrlParameter.Optional });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
