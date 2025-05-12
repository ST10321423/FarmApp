using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmApp.Controllers
{
    public class FarmerController : Controller
    {
        // GET: Farmer
        public ActionResult Index()
        {
            return View();
        }
    }
}