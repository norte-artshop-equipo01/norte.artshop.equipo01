using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artshop.Data.Data.Managers
{
    public class CartItemManager
    {
        private readonly IDatabaseData _database;

        public CartItemManager(IDatabaseData database)
        {
            _database = database;
        }

        public void AddNewCartItem(CartItem cartitem)
        {
            _database.Add(cartitem);
        }

        public void RemoveCartItem(CartItem cartitem)
        {
            _database.Remove(cartitem);
        }

        public void UpdateCarItem(CartItem cartitem)
        {
            _database.Update(cartitem);
        }

        public List<CartItem> FindCartItem(Func<CartItem, bool> cartitem)
        {
            return _database.Find(cartitem);
        }

        public List<CartItem> GetAllCartItem(int cartId)
        {
            return _database.GetAll<CartItem>().Where(x => x.CartId == cartId).ToList();
        }
    }
}
