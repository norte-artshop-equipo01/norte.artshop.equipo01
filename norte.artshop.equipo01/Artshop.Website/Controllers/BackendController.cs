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
{   [Authorize(Roles = "Administrator")]
   
    public class BackendController : Controller
    {
        private readonly DatabaseConnection db;
        public BackendController()
        {
            db = new DatabaseConnection(ConnectionType.Database, WebConfigurationManager.ConnectionStrings["somee"].ToString());
        }
        // GET: Backend
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ABM2()
        {
            return View();
        }
        public ActionResult AbmArtistas()
        {
            return View();
        }
        public ActionResult AbmProducto()
        {
            //List<Artist> artistas = this.db.ArtistManager.GetAllArtists();

            List<SelectListItem> artistas = db.ArtistManager.GetAllArtists().ConvertAll(
                d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.FirstName+" "+d.LastName,
                        Value = d.Id.ToString()
                    };
                }).ToList();
                ViewBag.artistas = artistas;

            return View();
        }

        [HttpPost]
        public ActionResult Create2(Artist artista)
        {
            
            UpdateModel(artista);
            artista.CreatedOn = DateTime.Now;
            artista.ChangedOn = DateTime.Now;
            artista.CreatedBy = User.Identity.Name;
            artista.ChangedBy = User.Identity.Name;
            db.ArtistManager.AddNewArtist(artista);
          
            return RedirectToAction("ABM2");
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
            newartist.CreatedOn = DateTime.Now;
            newartist.ChangedOn = DateTime.Now;
            newartist.CreatedBy = User.Identity.Name;
            newartist.ChangedBy = User.Identity.Name;
            db.ArtistManager.AddNewArtist(newartist);
            return RedirectToAction("AbmArtistas");
        }
        [HttpPost]
        public ActionResult CreateObra(Product product)
        {
            
            UpdateModel(product);
            
            product.QuantitySold = 0;
            product.AvgStars = 0;
            product.CreatedOn = DateTime.Now;
            product.ChangedOn = DateTime.Now;
            product.CreatedBy = User.Identity.Name;
            product.ChangedBy = User.Identity.Name;
            db.ProductManager.AddNewProduct(product);

            return RedirectToAction("AbmProducto");
        }
        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase uploadFile)
        {
            if (uploadFile.ContentLength > 0)
            {
                string relativePath = "~/Content/Images/Products/" + Path.GetFileName(uploadFile.FileName);
                string physicalPath = Server.MapPath(relativePath);
                uploadFile.SaveAs(physicalPath);
                //return View((object)relativePath);
            }
            List<SelectListItem> artistas = db.ArtistManager.GetAllArtists().ConvertAll(
                d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.FirstName + " " + d.LastName,
                        Value = d.Id.ToString()
                    };
                }).ToList();
            ViewBag.artistas = artistas;

            return View("AbmProducto");
        }

        
    }
}