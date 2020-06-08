using art_shop_core;
using art_shop_core.EntityFramework;
using art_shop_core.DAL;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace Lppa.WebSite.Controllers
{
    public class HomeController : Controller
    {
        List<Product> obrasdev = new List<Product>();
        Core core;
        public HomeController()
        {
            core = new Core(new EntityFrameworkConnection("art-shop-model", "edu-spark-art", @"LY2PRO"));
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "La página de descripción de su aplicación.";
            return View();
        }
        public ActionResult Artistas()
        {
            ViewBag.Message = "La página de descripción de su aplicación.";
            return View();
        }
        public ActionResult ArtistasUser()
        {
            ViewBag.Message = "La página de descripción de su aplicación.";
            return View();
        }
        public ActionResult Galeria()
        {
            ViewBag.Message = "La página de descripción de su aplicación.";
           /* test();*/
            
            //EntityFrameworkConnection conn = new EntityFrameworkConnection("art-shop-model", "edu-spark-art", @"localhost\SQLEXPRESS");
            //List<Product> obrasdev = conn.Find<Product>(product => product.Id > 0);
            return View(core.ProductManager.GetAllProducts());
            // return PartialView(obrasdev);
            
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Su página de contacto.";
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection artista)
        {
            return RedirectToAction("Index");
        }

        [WebMethod]
        public List<Product> Test()
        {
            var efConn = new EntityFrameworkConnection("art-shop-model", "edu-spark-art", @"LOCALHOST\SQLEXPRESS");
            
            var obras = efConn.Find<Product>(product => product.Id > 0);
            foreach (var item in obras)
            {
                var file = item.Image;
            }
            return  obras;
        }
        
    }
}