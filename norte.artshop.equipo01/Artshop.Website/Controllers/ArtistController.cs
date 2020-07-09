using Artshop.Data.Data;
using Artshop.Data.Data.EntityFramework;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Artshop.Website.Controllers
{
    public class ArtistController : BaseController
    {
        private readonly DatabaseConnection db;
        public ArtistController()
        {
            db = new DatabaseConnection(ConnectionType.Database, WebConfigurationManager.ConnectionStrings["somee"].ToString());
        }
        public ActionResult Index()
        {
            return View(db.ArtistManager.GetAllArtists());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Artist artista)
        {
            CheckAuditPattern(artista, true);
            var listModel = db.ValidateModel(artista);
            if (ModelIsValid(listModel))
                return View(artista);



            try
            {
                db.ArtistManager.AddNewArtist(artista);
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.MessageDanger = "Chequear Datos ingresados";
                return View(artista);
            }
            
            

        }
    }
}