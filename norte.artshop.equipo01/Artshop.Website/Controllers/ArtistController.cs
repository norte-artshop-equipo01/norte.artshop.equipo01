using Artshop.Data.Data;
using Artshop.Data.Data.EntityFramework;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Artshop.Website.Controllers
{
    [Authorize(Roles = "Administrator")]
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
        /*[HttpPost]
        public ActionResult _CreatePartial(Artist artista)
        {
            CheckAuditPattern(artista, true);
            var listModel = db.ValidateModel(artista);
            if (ModelIsValid(listModel))
            {
                
                ViewBag.Message = "Chequear Datos ingresados";
                return RedirectToAction("Index");
            }
            try
            {
                db.ArtistManager.AddNewArtist(artista);
                ViewBag.Message = "Artista Ingresado" + " "+ artista.FullName;
                return View("Index", db.ArtistManager.GetAllArtists());
            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.Message = "Chequear Datos ingresados1";
                return View("Index", db.ArtistManager.GetAllArtists());
               
               
            }



    }*/
        public ActionResult Delete(int id)
        {
            var artista = db.ArtistManager.FindArtist(x => x.Id == id).FirstOrDefault();
            if (artista == null)
            {
                ViewBag.Messagealert = "El artista no se encuentra";
                return View("Index", db.ArtistManager.GetAllArtists());
            }
            try
            {
                artista.Disabled = true;
                db.ArtistManager.UpdateArtist(artista);
                
            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.Messagealert = "No se pudo eliminar el artista " + artista.FullName;
                return View("Index", db.ArtistManager.GetAllArtists());
            }
            try
            {
                for (int i = 0; i < artista.Product.Count; i++)
                {
                    var item = db.ProductManager.FindProduct(new Func<Product, bool>(x => x.Id == artista.Product.ElementAt(i).Id)).FirstOrDefault();
                    item.Disabled = true;
                    db.ProductManager.UpdateProduct(item);
                }
            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.Messagealert = "No se pudo eliminar al menos un producto del artista " + artista.FullName;
                return View("Index", db.ArtistManager.GetAllArtists());
            }
            ViewBag.Messagealert = "Artista " + artista.FullName + " fue eliminado";
            return View("Index", db.ArtistManager.GetAllArtists());

        }
        [HttpPost]
        public ActionResult Create(FormCollection artista)
        {
            var newartist = new Artist();
            UpdateModel(newartist);
            newartist.FirstName = artista["FirstName"];
            newartist.LastName = artista["LastName"];
            newartist.LifeSpan = artista["LifeSpan"];
            newartist.Country = artista["Country"];
            newartist.Description = artista["Description"];
            CheckAuditPattern(newartist, true);
           

            try
            {
                db.ArtistManager.AddNewArtist(newartist);
                
            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.Messagealert = "Chequear Datos ingresados excepcion nueva";
                return View("Index", db.ArtistManager.GetAllArtists()); ;
            }

            ViewBag.Message="Artista " + newartist.FullName + " ingresado correctamente";
            return View("Index", db.ArtistManager.GetAllArtists());
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var artist = db.ArtistManager.FindArtist(x=>x.Id==id).FirstOrDefault();
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        [HttpPost]
        public ActionResult Edit(Artist artista)
        {
            this.CheckAuditPattern(artista,false);
            var listModel = db.ValidateModel(artista);
            if (ModelIsValid(listModel))
                return View(artista);
            try
            {
                db.ArtistManager.UpdateArtist(artista);
                
            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.MessageDanger = ex.Message;
                return View(artista);
            }
            ViewBag.MessageDanger = "El artita " + artista.FullName + " fue actualizado con éxito";
                return View("Index", db.ArtistManager.GetAllArtists());
        }


    }
}