using Artshop.Data.Data;
using Artshop.Data.Data.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artshop.Tests
{
    [TestClass]
    public class EntityFrameworkTests
    {
        private readonly string connectionString;

        public EntityFrameworkTests()
        {
            connectionString = ConfigurationManager.ConnectionStrings["test"].ToString();
        }

        private void PostTestCleanup(DatabaseConnection database)
        {
            database.RunCustomCommand("delete from [Product]");
            database.RunCustomCommand("delete from [Artist]");
        }

        [TestMethod]
        public void Should_Store_Entity()
        {
            // Arrange
            var database = new DatabaseConnection(ConnectionType.Database, connectionString);
            var initialCount = database.ArtistManager.GetAllArtists().Count;
            var artist = TestUtils.GetArtists()[0];

            // Act
            database.ArtistManager.AddNewArtist(artist);

            // Assert
            var finalCount = database.ArtistManager.GetAllArtists().Count;
            Assert.AreEqual(initialCount + 1, finalCount);

            PostTestCleanup(database);
        }

        [TestMethod]
        public void Should_Remove_Entity()
        {
            // Arrange
            var database = new DatabaseConnection(ConnectionType.Database, connectionString);
            var initialCount = database.ArtistManager.GetAllArtists().Count;
            var artist = TestUtils.GetArtists()[0];
            database.ArtistManager.AddNewArtist(artist);

            // Act
            database.ArtistManager.RemoveArtist(artist);

            // Assert
            var finalCount = database.ArtistManager.GetAllArtists().Count;
            Assert.AreEqual(initialCount, finalCount);

            PostTestCleanup(database);
        }

        [TestMethod]
        public void Should_Find_Entity()
        {
            // Arrange
            var database = new DatabaseConnection(ConnectionType.Database, connectionString);
            var artists = TestUtils.GetArtists();
            artists.ForEach(x => database.ArtistManager.AddNewArtist(x));

            // Act
            var storedOrder = database.ArtistManager.FindArtist(new Func<Artist, bool>(x => x.LastName == "Botticelli")).FirstOrDefault();

            // Assert
            Assert.IsNotNull(storedOrder);
            
            PostTestCleanup(database);
        }

        [TestMethod]
        public void Should_Update_Entity()
        {
            // Arrange
            var database = new DatabaseConnection(ConnectionType.Database, connectionString);
            var artist = TestUtils.GetArtists()[0];
            database.ArtistManager.AddNewArtist(artist);
            artist.LifeSpan = "60";

            // Act
            database.ArtistManager.UpdateArtist(artist);

            // Assert
            var storedArtist = database.ArtistManager.GetAllArtists()[0];
            Assert.AreEqual("60", storedArtist.LifeSpan);

            PostTestCleanup(database);
        }

        [TestMethod]
        public void Should_Store_Complex_Entities()
        {
            // Arrange
            var database = new DatabaseConnection(ConnectionType.Database, connectionString);
            var artist = TestUtils.GetArtists()[0];
            database.ArtistManager.AddNewArtist(artist);
            var storedArtist = database.ArtistManager.FindArtist(x => x.LastName == artist.LastName).FirstOrDefault();

            var product = TestUtils.GetProducts()[0];
            product.Artist = artist;

            // Act
            database.ProductManager.AddNewProduct(product);

            // Assert
            var storedProduct = database.ProductManager.FindProduct(x => x.Title == product.Title).FirstOrDefault();
            Assert.AreEqual(artist.Id, storedProduct.ArtistId);

            PostTestCleanup(database);
        }

        [TestMethod]
        public void Should_Return_Complex_Entities()
        {
            // Arrange
            var database = new DatabaseConnection(ConnectionType.Database, connectionString);
            var artist = TestUtils.GetArtists()[0];
            database.ArtistManager.AddNewArtist(artist);

            var product = TestUtils.GetProducts()[0];
            product.Artist = artist;

            // Act
            database.ProductManager.AddNewProduct(product);

            // Assert
            var storedProduct = database.ProductManager.FindProduct(x => x.Title == product.Title).FirstOrDefault();
            Assert.IsNotNull(storedProduct.Artist);

            PostTestCleanup(database);
        }
    }
}
