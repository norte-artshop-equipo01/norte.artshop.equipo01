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
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Index()
        {
            
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

            return View(db.ProductManager.GetAllProducts());
        }

        [HttpPost]
        public ActionResult Create(FormCollection obra, HttpPostedFile Imageback)
        {
            var product = new Product();
            UpdateModel(product);
            try
            {
                if (Imageback.ContentLength > 0 && Imageback!=null)
                {
                    string filename = Path.GetFileName(Imageback.FileName);
                    string path = Path.Combine(Server.MapPath("/content/Images/products"), filename);
                    Imageback.SaveAs(path);

                    product.Image = filename;
                    CheckAuditPattern(product, true); ;
                    db.ProductManager.AddNewProduct(product);
                                   
                  
                }
            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.MessageDanger = "¡Error al cargar el Producto con su imagén.";
                List<SelectListItem> artistas1= db.ArtistManager.GetAllArtists().ConvertAll(
                d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.FirstName + " " + d.LastName,
                        Value = d.Id.ToString()
                    };
                }).ToList();
                ViewBag.artistas = artistas1;
                return View("Idex", db.ProductManager.GetAllProducts());
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
            ViewBag.Message = "Producto cargado correctamente.";
            return View("Idex",db.ProductManager.GetAllProducts());

        }
    }
}