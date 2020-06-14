﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artshop.Data.Data.EntityFramework
{
    public class EntityFrameworkData : IDatabaseData
    {
        public Entities entities;

        public EntityFrameworkData()
        {
            var entityString = new EntityConnectionStringBuilder()
            {
                Provider = "System.Data.SqlClient",
                Metadata = @"res://*/Data.EntityFramework.SparkArt.csdl|res://*/Data.EntityFramework.SparkArt.ssdl|res://*/Data.EntityFramework.SparkArt.msl",
                ProviderConnectionString = "data source=NOTEBOOK;initial catalog=edu-spark-art;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"
            };

            entities = new Entities(entityString.ConnectionString);
        }

        public void Add<T>(T item) where T : class
        {
            entities.Set<T>().Add(item);
            entities.SaveChanges();
        }

        public void CloseConnection()
        {
            entities.Database.Connection.Close();
        }

        public List<T> Find<T>(Func<T, bool> filter) where T : class
        {
            return entities.Set<T>().Where(filter).ToList();
        }

        public List<T> GetAll<T>() where T : class
        {
            return entities.Set<T>().ToList();
        }

        public void Remove<T>(T item) where T : class
        {
            entities.Set<T>().Remove(item);
            entities.SaveChanges();
        }

        public bool TestConnection()
        {
            return entities.Database.Exists();
        }

        public void Update<T>(T item) where T : class
        {
            entities.Entry(item).State = EntityState.Modified;
            entities.SaveChanges();
        }
    }
}