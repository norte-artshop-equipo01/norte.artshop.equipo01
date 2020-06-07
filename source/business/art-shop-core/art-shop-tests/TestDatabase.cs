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
        private readonly string modelName = "art-shop-model";
        private readonly string databaseName = "edu-spark-art";
        private readonly string connectionString = @"localhost\SQLEXPRESS";

        private IDatabaseConnection GetEntityFrameworkConnection()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return new EntityFrameworkConnection(configuration, modelName, databaseName, connectionString);
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

        private User GetTestUser()
        {
            return new User
            {
                FirstName = "Test First Name",
                LastName = "Test Last Name",
                Email = "testuser@spark.com",
                SignupDate = DateTime.Now,
                CreatedOn = DateTime.Now,
                ChangedOn = DateTime.Now,
                OrderCount = 0,
                CreatedBy = "admin",
                ChangedBy = "admin",
                City = "Buenos Aires",
                Country = "Argentina"
            };
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
            var efConn = new EntityFrameworkConnection(modelName, databaseName, connectionString);
            Assert.IsTrue(efConn.TestConnection());
        }

        [TestMethod]
        public void EntityFrameworkConnection_Should_Save_Entity()
        {
            var testArtist = GetTestArtist();
            var core = new Core(new EntityFrameworkConnection(modelName, databaseName, connectionString));
            
            core.ArtistManager.AddNewArtist(testArtist);
            var result = core.ArtistManager.FindArtist(artist => artist.FirstName == "Test Artist Name");
            Assert.AreEqual(result.Count, 1);

            core.ArtistManager.RemoveArtist(testArtist);
            core.CloseDatabaseConnection();
        }

        [TestMethod]
        public void EntityFrameworkConnection_Should_Update_Entity()
        {
            var testArtist = GetTestArtist();
            var core = new Core(new EntityFrameworkConnection(modelName, databaseName, connectionString));

            core.ArtistManager.AddNewArtist(testArtist);
            testArtist.LastName = "Updated Last Name";
            core.ArtistManager.UpdateArtist(testArtist);
            var result = core.ArtistManager.FindArtist(artist => artist.LastName == "Updated Last Name");
            Assert.AreEqual(result.Count, 1);

            core.ArtistManager.RemoveArtist(testArtist);
            core.CloseDatabaseConnection();
        }

        [TestMethod]
        public void EntityFrameworkConnection_Should_Return_All_Entities()
        {
            var firstArtist = new Artist
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

            var secondArtist = new Artist
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

            var core = new Core(new EntityFrameworkConnection(modelName, databaseName, connectionString));

            core.ArtistManager.AddNewArtist(firstArtist);
            core.ArtistManager.AddNewArtist(secondArtist);
            var result = core.ArtistManager.GetAllArtists();
            Assert.AreEqual(result.Count, 2);

            core.ArtistManager.RemoveArtist(firstArtist);
            core.ArtistManager.RemoveArtist(secondArtist);
            core.CloseDatabaseConnection();
        }

        [TestMethod]
        public void EntityFrameworkConnection_Should_Store_New_User()
        {
            var core = new Core(new EntityFrameworkConnection(modelName, databaseName, connectionString));
            core.UserManager.LogIn("admin", "admin");
            
            core.UserManager.AddNewUser("test first name", "test last name", "test email", "password");
            var result = core.UserManager.FindUser(user => user.FirstName == "test first name");
            Assert.AreEqual(result.Count, 1);

            core.UserManager.RemoveUser(result.FirstOrDefault());
            core.CloseDatabaseConnection();
        }
    }
}
