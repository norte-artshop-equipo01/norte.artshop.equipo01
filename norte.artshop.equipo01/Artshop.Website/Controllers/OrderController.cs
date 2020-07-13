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
        public double cant_items(List<CartItem> cart)
        {
            // var cart = obtener_cart(User.Identity.Name);
            double suma = 0;
            for (int i = 0; i < cart.Count(); i++)
            {
                suma += cart.ElementAt(i).Quantity;
            }
            return suma;
        }
        private List<CartItem> obtener_cart(string user)
        {
            var carrito = db.CartManager.FindCartByCookie(user);

            return db.CartItemManager.GetAllCartItem(carrito.Id);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Pago()
        {
            var cart=obtener_cart(User.Identity.Name);
            var user = db.UserManager.FindUser(x => x.Email == User.Identity.Name).FirstOrDefault();
            int cartid = cart.ElementAt(0).CartId;
            if (user == null)
            {
                User userbase = new User();
                userbase.Country = "pais";
                userbase.FirstName = "Nombre";
                userbase.LastName = "Apellido";
                userbase.Email = User.Identity.Name;
                userbase.City = "Ciudad";
                userbase.SignupDate = DateTime.Now;
                userbase.Password = sha256_hash("lrpm3276");
                userbase.OrderCount = 0;
                CheckAuditPattern(userbase, true);
                return View("Userreg", userbase);
            }
            OrderNumber number = new OrderNumber();
            
            CheckAuditPattern(number, true);
            db.OrderNumberManager.AddNewOrderNumber(number);
            number.Number = number.Id;
            Order order = new Order();
            
            order.OrderDate= DateTime.Now;
            order.TotalPrice = 0;
            number.Number=number.Id;
            order.OrderNumber = number.Number;
            order.ItemCount = 0;
            order.UserId = user.Id;
            CheckAuditPattern(order, true);
            try
            {
                db.OrderManager.AddNewOrder(order);
            }
            catch (Exception)
            {

                throw;
            }
            order = db.OrderManager.FindOrder(x=>x.OrderNumber==order.OrderNumber).FirstOrDefault();


            
            for (int i = 0; i < cart.Count; i++)
            {
                var orderdet = new OrderDetail();
                orderdet.OrderId = order.Id;
                orderdet.ProductId = cart.ElementAt(i).ProductId;
                orderdet.Quantity = cart.ElementAt(i).Quantity;
                orderdet.Price= cart.ElementAt(i).Price;
                CheckAuditPattern(orderdet, true);
                order.TotalPrice += cart.ElementAt(i).Total;
                order.ItemCount += cart.ElementAt(i).Quantity;
                try
                {
                    order.OrderDetail.Add(orderdet);
                    db.CartItemManager.RemoveCartItem(cart.ElementAt(i));
                    db.OrderManager.UpdateOrder(order);
                }
                catch (Exception ex)
                {
                    db.Logger(ex, System.Web.HttpContext.Current);

                }
                orderdet = null;
            }
            var cart2 = db.CartItemManager.FindCartItem(x => x.CartId == cart.ElementAt(0).CartId).FirstOrDefault();
            if (cart2!=null)
            {
                ViewBag.MessageAlert = "Quedaron items pendientes en el carrito. Se procesaron " + order.ItemCount + " artículos.";
                
                return View("Pago_conf");
            }
            else
            {
                db.CartManager.RemoveCart(db.CartManager.FindCart(x => x.Id == cartid).FirstOrDefault());
                ViewBag.Message = "Se proceso la orden " + order.OrderNumber + ". Por un monto total de $" + order.TotalPrice;
                return View("Pago_conf");
            }


           
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
            User user = new User();
             user =  db.UserManager.FindUser(x => x.Email == User.Identity.Name).FirstOrDefault();
            if(user==null)
            {
                User userbase = new User();
                //userbase.Country = "pais";
                //userbase.FirstName = "Nombre";
                //userbase.LastName = "Apellido";

                //userbase.City = "Ciudad";
                userbase.SignupDate = DateTime.Now;
                
                userbase.OrderCount = 0;
                userbase.Password= sha256_hash("lrpm3276");
                userbase.Email = User.Identity.Name;
                CheckAuditPattern(userbase, true);
                return View("Userreg", userbase);
            }
            else 
            {
                return View("Userreg", user);
                //ViewBag.User = user;
                //ViewBag.Total = sum_items(obtener_cart(User.Identity.Name));
                //return View("Index",obtener_cart(User.Identity.Name));
            }
           
        }


        [HttpPost]
        public ActionResult userreg(User user)
        {
            if(user.Id>0)
            { 
            CheckAuditPattern(user, false);
              try
               {
                    db.UserManager.UpdateUser(user);
                    
               }
              catch (Exception ex)
              {

                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.Messagealert = "Chequear Datos ingresados excepcion nueva";
                return View("userreg", user); 
                
                 }

            }
            else
            {
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
            }


            ViewBag.User = user;
            ViewBag.Total = sum_items(obtener_cart(User.Identity.Name));
            return View("Index",obtener_cart(User.Identity.Name));
        }
        [HttpPost]
        public ActionResult Pago_conf()
        {
            return View();
        }
    }
}