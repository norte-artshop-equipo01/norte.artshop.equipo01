using Artshop.Data.Data;
using Artshop.Website.Controllers;
using Artshop.Data.Data.EntityFramework;
using Artshop.Website.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace Artshop.Website.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        // GET: Order
        public ActionResult Index()
        {
            ViewBag.Total = sum_items(obtener_cart(User.Identity.Name));
            return View(obtener_cart(User.Identity.Name));
            
        }

        public double sum_items(List<CartItem> cart)
        {
            // var cart = obtener_cart(User.Identity.Name);
            double suma = 0;
            for (int i = 0; i < cart.Count(); i++)
            {
                suma += cart.ElementAt(i).Total;
            }
            return suma;
        }
        private List<CartItem> obtener_cart(string user)
        {
            var carrito = db.CartManager.FindCartByCookie(user);

            return db.CartItemManager.GetAllCartItem(carrito.Id);
        }
        [HttpPost]
        public ActionResult Pago()
        {
            var cart=obtener_cart(User.Identity.Name);
            Order order = new Order();
            order.OrderDate= DateTime.Now;
           
            return View();
        }


        public static Byte[] sha256_hash(String value)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
        }

        
        public ActionResult Userreg()
        {
            var user = db.UserManager.FindUser(x => x.Email == User.Identity.Name).FirstOrDefault();
            if(user==null)
            {
                User userbase = new User();
                userbase.Country = "pais";
                userbase.FirstName = "Nombre";
                userbase.LastName = "Apellido";
                userbase.Email = User.Identity.Name;
                userbase.City = "Ciudad";
                userbase.SignupDate = DateTime.Now;
                userbase.Password= sha256_hash("lrpm3276");
                userbase.OrderCount = 0;
                CheckAuditPattern(userbase, true);
                return View("Userreg", userbase);
            }
            else 
            {
                ViewBag.User = user;
                ViewBag.Total = sum_items(obtener_cart(User.Identity.Name));
                return View("Index",obtener_cart(User.Identity.Name));
            }
         
        }

        [HttpPost]
        public ActionResult userreg(User user)
        {

            CheckAuditPattern(user, true);
            try
            {
                db.UserManager.AddNewUser(user);
            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.Messagealert = "Chequear Datos ingresados excepcion nueva";
                return View("userreg", user); 
                
            }


            ViewBag.User = user;
            ViewBag.Total = sum_items(obtener_cart(User.Identity.Name));
            return View("Index",obtener_cart(User.Identity.Name));
        }

    }
}