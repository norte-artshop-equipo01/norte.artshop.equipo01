using Artshop.Data.Data;
using Artshop.Data.Data.EntityFramework;
using Artshop.Website.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Artshop.Website.Controllers
{
    
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Galeria()
        {
            ViewBag.Message = "La página de descripción de su aplicación.";

            return View(db.ProductManager.GetAllProducts());
        }
        
        public ActionResult Artistas()
        {
            ViewBag.Message = "La página de descripción de su aplicación.";
            return View(db.ArtistManager.GetAllArtists());
        }
       
        
        [Authorize]
        [HttpPost]
        public ActionResult Galeria(Product producto1)
        {
            
            //var temp =prod["Id"];
           
            
           var producto = db.ProductManager.FindProduct(new Func<Product, bool>(x => x.Id == producto1.Id)).FirstOrDefault();
            var carrito = db.CartManager.FindCartByCookie(User.Identity.Name);


            var item = new CartItem
            {   
                ProductId = producto.Id,
                Price = producto.Price,
                Quantity = 1
                
            };
            CheckAuditPattern(item);

            if (carrito == null)
            {
                NuevoCarrito(item);
            }
            else
            {
                item.CartId = carrito.Id;
                CarritoExistente(carrito, item);
            }
                       

            return View(db.ProductManager.GetAllProducts());
        }

        private void NuevoCarrito(CartItem cartItem)
        {
            var carrito = new Cart();
            carrito.CartDate = DateTime.Now;
            carrito.Cookie = User.Identity.Name;
           //cartItem.Cart = carrito;
            //cartItem.Quantity=1;
            carrito.CartItem.Add(cartItem);
            CheckAuditPattern(carrito, true);

            db.CartManager.AddNewCart(carrito);
        }

        private void CarritoExistente(Cart cart, CartItem cartItem)
        {

            bool flag = true;
            for (int i = 0; i < cart.CartItem.Count; i++)
            {
               
                if (cart.CartItem.ElementAt(i).ProductId == cartItem.ProductId)
                {
                    flag = false;


                    cart.CartItem.ElementAt(i).Quantity++;
                    db.CartManager.UpdateCart(cart);
                }
                             
            }
            if (flag) { 

            cart.CartItem.Add(cartItem);
            //cartItem.Cart = cart;
            db.CartManager.UpdateCart(cart);
            }
        }
        private List<CartItem> obtener_cart(string user)
        {
            var carrito = db.CartManager.FindCartByCookie(user);
            
            return db.CartItemManager.GetAllCartItem(carrito.Id);
        }
        [Authorize]
        public ActionResult Buy()
        {
            //var carrito = db.CartManager.FindCartByCookie(User.Identity.Name);
            ViewBag.Total=sum_items(obtener_cart(User.Identity.Name));
            return View(obtener_cart(User.Identity.Name));
            
        }

        public ActionResult DeleteItemCar(int id)
        {
            var item = db.CartItemManager.FindCartItem(x => x.Id == id).FirstOrDefault();
            if (item == null)
            {
                ViewBag.Messagealert = "El producto no se encuentra en el carrito, Actualizar la vista";
                ViewBag.Total = sum_items(obtener_cart(User.Identity.Name));
                return RedirectToAction("Buy", obtener_cart(User.Identity.Name));
            }
            try
            {
                
                db.CartItemManager.RemoveCartItem(item);
                
            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.Messagealert = "No se pudo eliminar la linea";
                ViewBag.Total = sum_items(obtener_cart(User.Identity.Name));
                return RedirectToAction("Buy",obtener_cart(User.Identity.Name));
            }

            ViewBag.Messagealert = "Se quito la obra  del carrito";
            ViewBag.Total = sum_items(obtener_cart(User.Identity.Name));
            return RedirectToAction("Buy", obtener_cart(User.Identity.Name));

        }
        private double sum_items(List<CartItem> cart)
        {
            // var cart = obtener_cart(User.Identity.Name);
            double suma = 0;
            for (int i = 0; i < cart.Count(); i++)
            {
                suma += cart.ElementAt(i).Total;
            }
            return suma;
        }

        [HttpPost]
        public ActionResult UpdateItemCar(FormCollection item)
        {
            var id = item["item.Id"];
            var cantidad = item["item.Quantity"];
            
           var cartitem= db.CartItemManager.FindCartItem(x => x.Id == Convert.ToInt16(id)).FirstOrDefault();
            this.CheckAuditPattern(cartitem, false);
            var listModel = db.ValidateModel(cartitem);
            if (ModelIsValid(listModel))
            {
                ViewBag.Total = sum_items(obtener_cart(User.Identity.Name));
                return RedirectToAction("Buy", obtener_cart(User.Identity.Name));
            }
            try
            {

                cartitem.Quantity = Convert.ToInt16(cantidad);
                db.CartItemManager.UpdateCarItem(cartitem);

            }
            catch (Exception ex)
            {
                db.Logger(ex, System.Web.HttpContext.Current);
                ViewBag.Messagealert = ex.Message;
                return View(obtener_cart(User.Identity.Name));
            }
            ViewBag.Messagealert = "Catidad de "+cartitem.Product.Title+" actualizada";
            ViewBag.Total = sum_items(obtener_cart(User.Identity.Name));
            return RedirectToAction("Buy", obtener_cart(User.Identity.Name));
        }

    }
}