using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;

namespace Artshop.Data.Data.Managers
{
    public class OrderManager
    {
        private readonly IDatabaseData _database;

        public OrderManager(IDatabaseData database)
        {
            _database = database;
        }

        public void AddNewOrder(Order order)
        {
            _database.Add(order);
        }

        public void RemoveOrder(Order order)
        {
            _database.Remove(order);
        }

        public void UpdateOrder(Order order)
        {
            _database.Update(order);
        }

        public List<Order> FindOrder(Func<Order, bool> filter)
        {
            return _database.Find(filter);
        }

        public List<Order> GetAllOrders()
        {
            return _database.GetAll<Order>();
        }
    }
}
