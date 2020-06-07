using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using art_shop_core.DAL;
using art_shop_core.EntityFramework;


namespace art_shop_core.EntityFramework
{
    public class EntityFrameworkConnection : IDatabaseConnection
    {
        public Entities entities;

        public EntityFrameworkConnection(Configuration configuration, string modelName, string databaseName, string dataSource)
        {
            var connectionStringSettings = new ConnectionStringSettings
            {
                Name = "Entities",
                ConnectionString = ConnectionStringTemplate(modelName, databaseName, dataSource),
                ProviderName = "System.Data.EntityClient"
            };

            configuration.ConnectionStrings.ConnectionStrings.Remove(connectionStringSettings);
            configuration.ConnectionStrings.ConnectionStrings.Add(connectionStringSettings);
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("ConnectionStrings");

            entities = new Entities("name=Entities");
        }

        public EntityFrameworkConnection(string modelName, string databaseName, string dataSource)
        {
            entities = new Entities(ConnectionStringTemplate(modelName, databaseName, dataSource));
        }

        private static string ConnectionStringTemplate(string modelName, string databaseName, string dataSource)
        {
            return $"metadata=res://*/EntityFramework.{modelName}.csdl|" +
                   $"res://*/EntityFramework.{modelName}.ssdl|" +
                   $"res://*/EntityFramework.{modelName}.msl;" +
                   "provider=System.Data.SqlClient;" +
                   $"provider connection string=\"data source={dataSource};" +
                   $"initial catalog={databaseName};" +
                   "integrated security=True;" +
                   "MultipleActiveResultSets=True;" +
                   "App=EntityFramework\"";
        }

        public void CloseConnection()
        {
            entities.Database.Connection.Close();
        }

        public bool TestConnection()
        {
            return entities.Database.Exists();
        }

        public void Add<T>(T item) where T : class
        {
            entities.Set<T>().Add(item);
            entities.SaveChanges();
        }

        public void Remove<T>(T item) where T : class
        {
            entities.Set<T>().Remove(item);
            entities.SaveChanges();
        }

        public void Update<T>(T item) where T : class
        {
            entities.Entry(item).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public List<T> Find<T>(Func<T, bool> filter) where T : class
        {
            return entities.Set<T>().Where(filter).ToList();
        }

        public List<T> GetAll<T>() where T : class
        {
            return entities.Set<T>().ToList();
        }
    }
}
