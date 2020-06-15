using System;
using System.Linq;
using Artshop.Data.Data;
using Artshop.Data.Data.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Artshop.Tests
{
    [TestClass]
    public class InMemoryTests
    {
        [TestMethod]
        public void Should_Store_Entity()
        {
            // Arrange
            var database = new DatabaseConnection(ConnectionType.InMemory);
            var artist = TestUtils.GetArtists()[0];

            // Act
            database.ArtistManager.AddNewArtist(artist);

            // Assert
            var total = database.ArtistManager.GetAllArtists().Count;
            Assert.AreEqual(1, total);
        }

        [TestMethod]
        public void Should_Remove_Entity()
        {
            // Arrange
            var database = new DatabaseConnection(ConnectionType.InMemory);
            var product = TestUtils.GetProducts()[0];
            database.ProductManager.AddNewProduct(product);

            // Act
            database.ProductManager.RemoveProduct(product);

            // Assert
            var total = database.ProductManager.GetAllProducts().Count;
            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void Should_Find_Entity()
        {
            // Arrange
            var database = new DatabaseConnection(ConnectionType.InMemory);
            var order = TestUtils.GetOrders()[0];
            database.OrderManager.AddNewOrder(order);

            // Act
            var storedOrder = database.OrderManager.FindOrder(new Func<Order, bool>(x => x.OrderNumber == 3)).FirstOrDefault();

            // Assert
            Assert.IsNotNull(storedOrder);
        }

        [TestMethod]
        public void Should_Update_Entity()
        {
            // Arrange
            var database = new DatabaseConnection(ConnectionType.InMemory);

            var user = TestUtils.GetUsers()[0];
            database.UserManager.AddNewUser(user);
            user.Email = "user@spark.com";

            // Act
            database.UserManager.UpdateUser(user);

            // Assert
            var storedEmail = database.UserManager.FindUser(new Func<User, bool>( x => x.Id == 0)).FirstOrDefault().Email;
            Assert.AreEqual("user@spark.com", storedEmail);
        }

        [TestMethod]
        public void Should_Return_All_Entities()
        {
            // Arrange
            var database = new DatabaseConnection(ConnectionType.InMemory);
            var botticelli = TestUtils.GetArtists()[0];
            var daVinci = TestUtils.GetArtists()[1];

            database.ArtistManager.AddNewArtist(botticelli);
            database.ArtistManager.AddNewArtist(daVinci);

            // Act
            var allArtists = database.ArtistManager.GetAllArtists();

            // Assert
            Assert.IsTrue(allArtists.Count > 1);
        }
    }
}
