using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artshop.Data.Data.InMemory
{
    internal class InMemoryData : IDatabaseData
    {
        private readonly Dictionary<string, object> database;

        public InMemoryData()
        {
            database = new Dictionary<string, object>
            {
                { nameof(Artist), new List<Artist>() },
                { nameof(Cart), new List<Cart>() },
                { nameof(CartItem), new List<CartItem>() },
                { nameof(Error), new List<Error>() },
                { nameof(Order), new List<Order>() },
                { nameof(OrderDetail), new List<OrderDetail>() },
                { nameof(OrderNumber), new List<OrderNumber>() },
                { nameof(Product), new List<Product>() },
                { nameof(Rating), new List<Rating>() },
                { nameof(User), new List<User>() },
            };
        }

        public void Add<T>(T item) where T : class
        {
            GetList<T>(typeof(T).Name).Add(item);
        }

        public void CloseConnection()
        {
            
        }

        public List<T> Find<T>(Func<T, bool> filter) where T : class
        {
            return GetList<T>(typeof(T).Name).Where(filter).ToList();
        }

        public List<T> GetAll<T>() where T : class
        {
            return GetList<T>(typeof(T).Name);
        }

        public void Remove<T>(T item) where T : class
        {
            var function = new Func<T, bool>(x => (int)typeof(T).GetProperty("Id").GetValue(x) == (int)typeof(T).GetProperty("Id").GetValue(item));
            var element = Find(function).FirstOrDefault();
            GetList<T>(typeof(T).Name).Remove(element);
        }

        public bool TestConnection()
        {
            return true;
        }

        public void Update<T>(T item) where T : class
        {
            Remove(item);
            Add(item);
        }

        private List<T> GetList<T>(string name) where T : class
        {
            return (List<T>)Convert.ChangeType(database[name], typeof(List<T>));
        }
    }
}
