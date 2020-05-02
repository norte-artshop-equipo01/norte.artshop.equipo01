using System;
using System.Collections.Generic;
using System.Configuration;
using art_shop_core.DAL;
using art_shop_core.EntityFramework;

namespace art_shop_core
{
    public class Database
    {
        private readonly IDatabaseConnection db;

        public Database(IDatabaseConnection connection)
        {
            db = connection;
            db.TestConnection();
        }

        public void CloseConnection()
        {
            db.CloseConnection();
        }

        public void Add<T>(T item) where T : class
        {
            db.Add(item);
        }

        public void Remove<T>(T item) where T : class
        {
            db.Remove(item);
        }

        public List<T> Find<T>(Func<T, bool> filter) where T : class
        {
            return db.Find(filter);
        }
    }
}
