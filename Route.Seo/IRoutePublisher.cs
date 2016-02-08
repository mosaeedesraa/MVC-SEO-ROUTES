using System.Web.Routing;

namespace Routes.Seo
{
    public interface IRoutePublisher
    {
        void RegisterRoutes(RouteCollection routeCollection);
    }
}