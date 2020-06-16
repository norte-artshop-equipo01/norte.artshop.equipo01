using Artshop.Data.Data;
using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Artshop.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly EntityFrameworkData entityFrameworkData;

        public HomeController()
        {
            entityFrameworkData = new EntityFrameworkData();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Galeria()
        {
            ViewBag.Message = "La página de descripción de su aplicación.";

            /*return View(Data.Managers..GetAllProducts());*/
            return View(entityFrameworkData.GetAll<Product>());
        }
        public ActionResult Artistas()
        {
            ViewBag.Message = "La página de descripción de su aplicación.";
            return View();
        }
        public ActionResult AbmArtistas()
        {
            ViewBag.Message = "La página de descripción de su aplicación.";
            return View();
        }
    }
}