using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seo.Route.Controllers
{
    public class EngineersController : BaseController
    {
        public ActionResult EngineerDetails(int EngineerId, string attributes)
        {
            return View(EngineerId);
        }

        public ActionResult EnginnerDetail(Int32 ID)
        {
            return View(ID);
        }
    }
}