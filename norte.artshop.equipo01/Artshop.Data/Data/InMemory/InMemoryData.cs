using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
                { nameof(AspNetRoles), new List<AspNetRoles>() },
                { nameof(AspNetUserClaims), new List<AspNetUserClaims>() },
                { nameof(AspNetUserLogins), new List<AspNetUserLogins>() },
                { nameof(AspNetUsers), new List<AspNetUsers>() },
            };
        }

        public void Add<T>(T item) where T : class
        {
            GetList<T>(typeof(T).Name).Add(item);
        }

        public void CloseConnection()
        {
            
        }

        public List<T> Find<T>(Func<T, bool> filter) where T : class => GetList<T>(typeof(T).Name).Where(filter).ToList();

        public List<T> GetAll<T>() where T : class => GetList<T>(typeof(T).Name);

        public void Remove<T>(T item) where T : class
        {
            var function = new Func<T, bool>(x => (int)typeof(T).GetProperty("Id").GetValue(x) == (int)typeof(T).GetProperty("Id").GetValue(item));
            var element = Find(function).FirstOrDefault();
            GetList<T>(typeof(T).Name).Remove(element);
        }

        public void RunCustomCommand(string command)
        {
            
        }

        public bool TestConnection() => true;

        public void Update<T>(T item) where T : class
        {
            Remove(item);
            Add(item);
        }

        private List<T> GetList<T>(string name) where T : class => (List<T>)Convert.ChangeType(database[name], typeof(List<T>));
    }
}
