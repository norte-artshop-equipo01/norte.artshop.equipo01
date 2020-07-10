using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Artshop.Data.Data.Managers
{
    public class CartManager
    {
        private readonly IDatabaseData _database;

        public CartManager(IDatabaseData database)
        {
            _database = database;
        }

        public void AddNewCart(Cart cart)
        {
            _database.Add(cart);
        }

        public void RemoveCart(Cart cart)
        {
            for (int i = 0; i < cart.CartItem.Count; i++)
            {
                _database.Remove(cart.CartItem.ElementAt(i));
            }
            
            _database.Remove(cart);
        }

        public void UpdateCart(Cart cart)
        {
            _database.Update(cart);
        }

        public List<Cart> FindCart(Func<Cart, bool> filter)
        {
            return _database.Find(filter);
        }

        public Cart FindCartByCookie(string cookie)
        {
            return _database.Find(new Func<Cart, bool> (c => c.Cookie == cookie)).FirstOrDefault();
        }

        public List<Cart> GetAllCarts()
        {
            return _database.GetAll<Cart>();
        }
    }
}
