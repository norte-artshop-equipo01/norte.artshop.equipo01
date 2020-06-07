using art_shop_core.EntityFramework;
using System;
using System.Collections.Generic;

namespace art_shop_core.DAL
{
    public class ProductManager
    {
        private readonly IDatabaseConnection database;

        public ProductManager(IDatabaseConnection database)
        {
            this.database = database;
        }

        public void AddNewProduct(Product product)
        {
            database.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            database.Remove(product);
        }

        public void UpdateProduct(Product product)
        {
            database.Update(product);
        }

        public List<Product> FindProduct(Func<Product, bool> filter)
        {
            return database.Find(filter);
        }

        public List<Product> GetAllProducts()
        {
            return database.GetAll<Product>();
        }
    }
}
