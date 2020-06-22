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
{   //[Authorize(Roles = "Administrator")]
   
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
            var newartist = new Artist();
            UpdateModel(artista);
            newartist = artista;
            newartist.CreatedOn = DateTime.Now;
            newartist.ChangedOn = DateTime.Now;
            newartist.CreatedBy = User.Identity.Name;
            newartist.ChangedBy = User.Identity.Name;
            db.ArtistManager.AddNewArtist(newartist);
            
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
            var newproduct = new Product();
            UpdateModel(product);
            newproduct = product;
            newproduct.QuantitySold = 0;
            newproduct.AvgStars = 0;
            newproduct.CreatedOn = DateTime.Now;
            newproduct.ChangedOn = DateTime.Now;
            newproduct.CreatedBy = User.Identity.Name;
            newproduct.ChangedBy = User.Identity.Name;
            db.ProductManager.AddNewProduct(newproduct);

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