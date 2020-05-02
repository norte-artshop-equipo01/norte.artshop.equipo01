using System;
using System.Configuration;
using System.Linq;
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
            return new EntityFrameworkConnection(configuration, "art-shop-model", "edu-spark-art-test", "NOTEBOOK");
        }

        private Database GetDatabase()
        {
            return new Database(GetEntityFrameworkConnection());
        }

        private Artist GetTestArtist()
        {
            return new Artist
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
        }

        [TestMethod]
        public void EntityFrameworkConnection_Should_Save_Entity()
        {
            var testArtist = GetTestArtist();
            var database = GetDatabase();
            
            database.Add(testArtist);
            var result = database.Find<Artist>(artist => artist.FirstName == "Test Artist Name");
            database.Remove(testArtist);
            database.CloseConnection();

            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void EntityFrameworkConnection_Should_Connect_Using_LocalConfiguration()
        {
            var efConn = GetEntityFrameworkConnection();
            Assert.IsTrue(efConn.TestConnection());
        }

        [TestMethod]
        public void EntityFrameworkConnection_Should_Connect_Without_Using_LocalConfiguration()
        {
            var efConn = new EntityFrameworkConnection("art-shop-model", "edu-spark-art-test", "NOTEBOOK"); ;
            Assert.IsTrue(efConn.TestConnection());
        }

        [TestMethod]
        public void EntityFrameworkConnection_Should_Update_Entity()
        {
            var testArtist = GetTestArtist();
            var database = GetDatabase();

            database.Add(testArtist);
            testArtist = database.Find<Artist>(artist => artist.FirstName == "Test Artist Name").FirstOrDefault();
            testArtist.LastName = "Updated Last Name";
            
            database.Update(testArtist);
            testArtist = database.Find<Artist>(artist => artist.FirstName == "Test Artist Name").FirstOrDefault();
            database.Remove(testArtist);
            database.CloseConnection();

            Assert.AreEqual(testArtist.LastName, "Updated Last Name");
        }
    }
}
