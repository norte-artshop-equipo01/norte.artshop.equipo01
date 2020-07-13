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
   
    public class BackendController : BaseController
    {   
        public ActionResult Index()
        {
            Request.Cookies.Add(new HttpCookie("spark-cookie") { Value = User.Identity.Name, Expires = DateTime.Now.AddDays(7) });
            
            return View();
        }
       
        public ActionResult AbmProducto()
        {
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