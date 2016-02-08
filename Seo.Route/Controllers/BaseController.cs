using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Seo.Route.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        { }
        protected virtual ActionResult RedirectToReferrer()
        {
            if (Request.UrlReferrer != null && string.IsNullOrEmpty(Request.UrlReferrer.ToString()))
            {
                return Redirect(Request.UrlReferrer.ToString());
            }

            return RedirectToRoute("HomePage");
        }
    }
}