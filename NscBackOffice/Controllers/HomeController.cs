using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NscBackOffice.Controllers
{

    [CustomAuthorize(AccessLevel = "Admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
