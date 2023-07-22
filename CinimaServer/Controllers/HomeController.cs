using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinimaServer.Models;

namespace CinimaServer.Controllers
{
    public class HomeController : Controller
    {
        HomeCinemaEntities db = new HomeCinemaEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //ViewBag.abc = db.Movies.FirstOrDefault().Title;
            return View();
        }
    }
}
