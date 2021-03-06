﻿using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

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
             for (int i = 0; i < product.CartItem.Count; i++)
            {
                _database.Remove(product.CartItem.ElementAt(i));
            }

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

        public List<Product> GetAllProducts(bool includeDisabled = false)
        {
            return !includeDisabled
                ? _database.GetAll<Product>().Where(x => x.Disabled == false).ToList()
                : _database.GetAll<Product>();
        }
    }
}
