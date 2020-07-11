using Artshop.Data.Data;
using Artshop.Data.Data.EntityFramework;
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
        public ActionResult AbmArtistas()
        {
            ViewBag.Message = "La página de descripción de su aplicación.";
            return View();
        }

        public ActionResult Buy(int id)
        {
            var producto = db.ProductManager.FindProduct(new Func<Product, bool>(x => x.Id == id)).FirstOrDefault();
            var item = new CartItem
            {
                ProductId = producto.Id,
                Price = producto.Price
            };
            CheckAuditPattern(item);

            var carrito = db.CartManager.FindCartByCookie(User.Identity.Name);
            if (carrito == null)
            {
                NuevoCarrito(item);
            }
            else
            {
                CarritoExistente(carrito, item);
            }
            
            return View();
        }

        private void NuevoCarrito(CartItem cartItem)
        {
            var carrito = new Cart();
            carrito.CartItem.Add(cartItem);
            carrito.CartDate = DateTime.Now;
            carrito.Cookie = User.Identity.Name;
            cartItem.Cart = carrito;
            cartItem.Quantity++;
            CheckAuditPattern(carrito, true);

            db.CartManager.AddNewCart(carrito);
        }

        private void CarritoExistente(Cart cart, CartItem cartItem)
        {
            for (int i = 0; i < cart.CartItem.Count; i++)
            {
                if (cart.CartItem.ElementAt(i).ProductId == cartItem.ProductId)
                {
                    cart.CartItem.ElementAt(i).Quantity++;
                    db.CartManager.UpdateCart(cart);
                    return;
                }
            }

            cart.CartItem.Add(cartItem);
            cartItem.Cart = cart;
            db.CartManager.UpdateCart(cart);
        }
    }
}