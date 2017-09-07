using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IContactDAL _contactDAL;

        public HomeController(IContactDAL contactDAL)
        {
            _contactDAL = contactDAL;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}