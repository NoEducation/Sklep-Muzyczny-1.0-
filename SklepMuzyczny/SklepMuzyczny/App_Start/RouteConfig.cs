using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SklepMuzyczny
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("",
                "Piosenki/{categoryName}/Strona-{page}",
                new { controller = "Song", action = "SongsByCategory" });

            routes.MapRoute("",
                "Piosenki/{categoryName}",
                new { controller = "Song", action = "SongsByCategory" }
                );

            routes.MapRoute("",
                "Piosenki/Szczegoly/{songId}"
                , new { action = "SongDetails", controller = "Song" });


            routes.MapRoute("",
                "Koszyk/Twoje-produkty",
                new { controller = "Cart", action = "Index" });

            routes.MapRoute("",
                "Koszyk/Skladanie-zamowienia",
                new { controller = "Cart", action = "Payment" });

            routes.MapRoute("",
                "Koszyk/Skladnie-zamowienia/Potwierdzenie",
                new { controller = "Cart", action = "Thanks" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
