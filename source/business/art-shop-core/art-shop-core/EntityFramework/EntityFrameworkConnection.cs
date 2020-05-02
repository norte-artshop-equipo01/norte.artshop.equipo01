using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using art_shop_core.DAL;

namespace art_shop_core.EntityFramework
{
    public class EntityFrameworkConnection : IDatabaseConnection
    {
        private Entities entities;

        public EntityFrameworkConnection(Configuration configuration, string modelName, string databaseName, string dataSource)
        {
            var connectionStringSettings = new ConnectionStringSettings
            {
                Name = "Entities",
                ConnectionString = $"metadata=res://*/EntityFramework.{ modelName }.csdl|" +
                                   $"res://*/EntityFramework.{ modelName }.ssdl|" +
                                   $"res://*/EntityFramework.{ modelName }.msl;" +
                                   "provider=System.Data.SqlClient;" +
                                   $"provider connection string=\"data source={dataSource};" +
                                   $"initial catalog={ databaseName };" +
                                   "integrated security=True;" +
                                   "MultipleActiveResultSets=True;" +
                                   "App=EntityFramework\"",
                ProviderName = "System.Data.EntityClient"
            };

            configuration.ConnectionStrings.ConnectionStrings.Remove(connectionStringSettings);
            configuration.ConnectionStrings.ConnectionStrings.Add(connectionStringSettings);
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("ConnectionStrings");

            entities = new Entities();
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

        public List<T> Find<T>(Func<T, bool> filter) where T : class
        {
            return entities.Set<T>().Where(filter).ToList();
        }
    }
}
