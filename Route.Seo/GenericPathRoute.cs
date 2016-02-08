using System.Web;
using System.Web.Routing;
namespace Routes.Seo
{
    /// <summary>
    /// Provides properties and methods for defining a SEO friendly route, and for getting information about the route.
    /// </summary>
    public class GenericPathRoute : LocalizedRoute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern and handler class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern, handler class and default parameter values.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern, handler class, default parameter values and constraints.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern, handler class, default parameter values, 
        /// constraints,and custom values.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="dataTokens">Custom values that are passed to the route handler, but which are not used to determine whether the route matches a specific URL pattern. The route handler might need these values to process the request.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// You can take this information from cache or table from database as you like
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetBySlug(string slug , RouteData data)
        {
            // you can take it from cache or sql table every ID and it's value for URL
            if (slug == "Eng-Mohamad-Alsaid")
            {
                data.Values["SeName"] = slug;
                data.Values["controller"] = "Engineers";
                data.Values["action"] = "EngineerDetails";
                data.Values["Engineerid"] = 1;
                return "OK";
            }
            else if (slug == "Syria")
            {
                data.Values["SeCountry"] = slug;
                data.Values["controller"] = "Engineers";
                data.Values["action"] = "EngineerDetails";
                data.Values["Engineerid"] = 2;
                return "OK";
            }
            else if (slug == "0090538972117")
            {
                data.Values["SeMobile"] = slug;
                data.Values["controller"] = "Engineers";
                data.Values["action"] = "EngineerDetails";
                data.Values["Engineerid"] = 3;
                return "OK";
            }
            else
                 return null;
        }

        /// <summary>
        /// Returns information about the requested route.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <returns>
        /// An object that contains the values from the route definition.
        /// </returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            // You can use sql database or cache to take request : 
            RouteData data = base.GetRouteData(httpContext);
            if (data != null)
            {
                var slug = data.Values["generic_se_name"] as string;
                //performance optimization.
                //we load a cached verion here. it reduces number of SQL requests for each page load
                var urlRecord = GetBySlug(slug , data);

                if (urlRecord == null)
                {
                    data.Values["controller"] = "Home";
                    data.Values["action"] = "Error";
                    return data;
                }
            }

            return data;
        }

        #endregion
    }
}