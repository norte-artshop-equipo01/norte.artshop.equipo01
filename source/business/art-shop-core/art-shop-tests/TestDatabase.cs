using System;
using System.Configuration;
using art_shop_core;
using art_shop_core.DAL;
using art_shop_core.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace art_shop_tests
{
    [TestClass]
    public class TestDatabase
    {
        private IDatabaseConnection GetEntityFrameworkConnection()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return new EntityFrameworkConnection(configuration, "art-shop-model", "edu-spark-art", "NOTEBOOK");
        }

        private Database GetDatabase()
        {
            return new Database(GetEntityFrameworkConnection());
        }

        [TestMethod]
        public void Should_Save_To_Database()
        {
            var testArtist = new Artist()
            {
                FirstName = "Test Artist Name",
                LastName = "Test Artist Name",
                ChangedBy = "Admin",
                ChangedOn = DateTime.Now,
                Country = "Argentina",
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now,
                Description = "Test Artist",
                Id = 0,
                LifeSpan = "Test Life Span",
                TotalProducts = 0
            };

            var database = GetDatabase();
            database.Add(testArtist);
            var result = database.Find<Artist>(artist => artist.FirstName == "Test Artist Name");
            database.Remove(testArtist);
            database.CloseConnection();

            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void EntityFrameworkConnection_Should_Check_Connection()
        {
            var efConn = GetEntityFrameworkConnection();
            Assert.IsTrue(efConn.TestConnection());
        }
    }
}
