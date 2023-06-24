using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HABERSİTEM
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(

                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Haber", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "Detay",
            url: "Detay/{Haberid}",
            defaults: new { controller = "Haber", action = "Detay" }
        );
        }
    }
}
