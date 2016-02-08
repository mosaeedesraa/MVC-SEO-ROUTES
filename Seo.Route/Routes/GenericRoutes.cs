using Routes.Seo;
using System.Web.Mvc;
using System.Web.Routing;


namespace Seo.Route.Routes
{
    public partial class SeoRoutes : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //generic URLs
            routes.MapGenericPathRoute("GenericUrl",
                "{*generic_se_name}",
                new { controller = "Common", action = "GenericUrl" },
                new[] { "Seo.Route.Controllers" });

            // Routes solely needed for URL creation, NOT for route matching.
            routes.MapLocalizedRoute("EngineerSeo1",
                "{SeName}",
                new { controller = "Engineers", action = "EngineerDetails" },
                new[] { "Seo.Route.Controllers" });

            routes.MapLocalizedRoute("EngineerSeo2",
                "{SeCountry}",
                new { controller = "Engineers", action = "EngineerDetails" },
                new[] { "Seo.Route.Controllers" });

            routes.MapLocalizedRoute("EngineerSeo3",
                "{SeMobile}",
                new { controller = "Engineers", action = "EngineerDetails" },
                new[] { "Seo.Route.Controllers" });
        }

        public int Priority
        {
            get
            {
                return int.MinValue;
            }
        }
    }
}
