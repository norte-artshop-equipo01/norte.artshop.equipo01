﻿using Artshop.Data.Data;
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
        public ActionResult Galeria(FormCollection prod)
        {
            
            var temp =prod["Id"];
           
            
           var producto = db.ProductManager.FindProduct(new Func<Product, bool>(x => x.Id == Convert.ToInt16(temp))).FirstOrDefault();
            var carrito = db.CartManager.FindCartByCookie(User.Identity.Name);
            
            var item = new CartItem
            {
                ProductId = producto.Id,
                Price = producto.Price
            };
            CheckAuditPattern(item);

            if (carrito == null)
            {
                NuevoCarrito(item);
            }
            else
            {
                CarritoExistente(carrito, item);
            }
                       

            return View(db.ProductManager.GetAllProducts());
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
        
        public ActionResult Buy()
        {
            return View(Listado_car());
        }

        private List<CheckoutItem> Listado_car()
        {
            var carrito = db.CartManager.FindCartByCookie(User.Identity.Name);
            var lista = new List<CheckoutItem>();
            if (carrito != null) 
            {
            
            foreach (var cartItem in carrito.CartItem)
            {
                var tempProd = db.ProductManager.FindProduct(new Func<Product, bool>(x => x.Id == cartItem.ProductId)).FirstOrDefault();
                lista.Add(new CheckoutItem
                {
                    ProductName = tempProd.Title,
                    ArtistName = tempProd.Artist.FullName,
                    Amount = cartItem.Quantity,
                    UnitPrice = tempProd.Price,
                    Image = tempProd.Image
                });
            }
            }
            return lista;
        }
    }
}