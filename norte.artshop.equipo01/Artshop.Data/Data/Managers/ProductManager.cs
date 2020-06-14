using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;

namespace Artshop.Data.Data.Managers
{
    public class ProductManager
    {
        private readonly IDatabaseData _database;

        public ProductManager(IDatabaseData database)
        {
            _database = database;
        }

        public void AddNewProduct(Product product)
        {
            _database.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            _database.Remove(product);
        }

        public void UpdateProduct(Product product)
        {
            _database.Update(product);
        }

        public List<Product> FindProduct(Func<Product, bool> product)
        {
            return _database.Find(product);
        }

        public List<Product> GetAllProducts()
        {
            return _database.GetAll<Product>();
        }
    }
}
