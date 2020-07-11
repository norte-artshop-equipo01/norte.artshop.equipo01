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
            
                ViewBag.Artistas = art_bag();
            
            return View(db.ProductManager.GetAllProducts());
        }

       [HttpPost]
        public ActionResult Create(FormCollection obra, HttpPostedFileBase Image2)
        { 
            var product = new Product();
            UpdateModel(product);
            try
            {
                if (Image2.ContentLength > 0 && Image2!=null)
                {
                    string filename = Path.GetFileName(Image2.FileName);
                    string path = Path.Combine(Server.MapPath("/content/Images/products"), filename);
                    Image2.SaveAs(path);
                    product.Image = filename;
                    CheckAuditPattern(product, true);
                    product.AvgStars = 0;
                    product.QuantitySold = 0;
                    db.ProductManager.AddNewProduct(product);                   
                }
            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.MessageDanger = "¡Error al cargar el Producto con su imagén.";
               
                ViewBag.Artistas = art_bag();
                return View("Index", db.ProductManager.GetAllProducts());
            }

            
            ViewBag.Artistas = art_bag();
            ViewBag.Message = "Producto cargado correctamente.";
            return View("Index",db.ProductManager.GetAllProducts());

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product= db.ProductManager.FindProduct(x => x.Id == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
          
            ViewBag.Artistas = art_bag(); 
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase Image2)
        {
            if (Image2 != null)
            { try
                {
                    string filename = Path.GetFileName(Image2.FileName);
                    string path = Path.Combine(Server.MapPath("/content/Images/products"), filename);
                    Image2.SaveAs(path);
                    product.Image = filename;
                }
                catch (Exception)
                {
                    ViewBag.MessageDanger = "No se pudo guardar la imagen";
                    return View(product);
                }
            
                              
            }
                this.CheckAuditPattern(product, false);
            var listModel = db.ValidateModel(product);
            if (ModelIsValid(listModel))
                return View(product);
            try
            {
                db.ProductManager.UpdateProduct(product);

            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.MessageDanger = ex.Message;
                ViewBag.Artistas = art_bag();
                return View(product);

            }
            ViewBag.Message = "La obra " + product.Title + " fue actualizada con éxito";
            ViewBag.Artitas = art_bag();
            return RedirectToAction("Index", db.ProductManager.GetAllProducts());
        }

        public ActionResult Delete(int id)
        {
            var producto = db.ProductManager.FindProduct(x => x.Id == id).FirstOrDefault();
            if (producto == null)
            {
                ViewBag.Messagealert = "El producto no se encuentra";
                return RedirectToAction("Index");
            }
            try
            {
                producto.Disabled = true;
                db.ProductManager.UpdateProduct(producto);
            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.Messagealert = "No se pudo eliminar la obra " + producto.Title;
                return RedirectToAction("Index");
            }
           
            ViewBag.Messagealert = "La obra " + producto.Title + " fue eliminada";
            return RedirectToAction("Index");

        }

        private List<SelectListItem> art_bag()
        {
            List<SelectListItem> artistas = db.ArtistManager.GetAllArtists().ConvertAll(
               d =>
               {
                   return new SelectListItem()
                   {
                       Text = d.FullName,
                       Value = d.Id.ToString()
                   };
               }).ToList();
           
            return artistas;

        }
    }
    
}