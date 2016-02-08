using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using Seo.Route.Controllers;
using Routes.Seo;


namespace Seo.Route.Routes
{
	public partial class GeneralRoutes : IRouteProvider
    {
		public void RegisterRoutes(RouteCollection routes)
		{
			
			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				new { controller = new IsKnownController() },
				new[] { "Seo.Route.Controllers" }
			);
		}

		public int Priority
		{
			get { return -999; }
		}
    }

	public class IsKnownController : System.Web.Routing.IRouteConstraint
	{
		private readonly static HashSet<string> s_knownControllers = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
		
		static IsKnownController()
		{
			var assembly = typeof(HomeController).Assembly;
			var controllerTypes = from t in assembly.GetExportedTypes()
								  where typeof(IController).IsAssignableFrom(t) && t.Namespace == "Seo.Route.Controllers"
								  select t;

			foreach (var type in controllerTypes)
			{
				var name = type.Name.Substring(0, type.Name.Length - 10);
				s_knownControllers.Add(name);
			}
		}
		
		public bool Match(HttpContextBase httpContext, System.Web.Routing.Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			object value;
			if (values.TryGetValue(parameterName, out value))
			{
				var requestedController = Convert.ToString(value);
				if (s_knownControllers.Contains(requestedController))
				{
					return true;
				}
			}
			return false;
		}
	}
}
