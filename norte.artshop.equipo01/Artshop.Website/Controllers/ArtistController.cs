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
        public ActionResult _CreatePartial(Artist artista)
        {
            CheckAuditPattern(artista, true);
            /*var listModel = db.ValidateModel(artista);
            if (ModelIsValid(listModel))
            {
                
                ViewBag.Message = "Chequear Datos ingresados";
                return RedirectToAction("Index");
            }*/
            try
            {
                db.ArtistManager.AddNewArtist(artista);
                ViewBag.Message = "Artista Ingresado" + " "+ artista.FullName;
                
            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.Message = "Chequear Datos ingresados";
                return RedirectToAction("Index");
               
               
            }
            return RedirectToAction("Index");


        }
        public ActionResult Delete(int id)
        {
            var artista = db.ArtistManager.FindArtist(x => x.Id == id).FirstOrDefault();
            if (artista == null)
            {
                ViewBag.Message = "El artista no se encuentra";
                return RedirectToAction("Index");
            }
            try
            {
                db.ArtistManager.RemoveArtist(artista);
                ViewBag.Message = "Artista eliminado";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.Message = "No se pudo eliminar el artista";
                return RedirectToAction("Index");
            }   
        }
       
       
    }
}