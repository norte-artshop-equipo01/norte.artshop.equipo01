﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lppa.WebSite.Controllers
{
    public class HomeController : Controller
    {
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
        public ActionResult Galeria()
        {
            ViewBag.Message = "La página de descripción de su aplicación.";
            return View();
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
    }
}