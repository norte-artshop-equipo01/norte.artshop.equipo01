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

            var artist = new Artist 
            {
                Id = 0,
                FirstName = "Sandro",
                LastName = "Botticelli",
                Country = "Argentina",
                LifeSpan = "50",
                Description = "Description",
                CreatedBy = "admin",
                ChangedBy = "admin",
                CreatedOn = DateTime.Now,
                ChangedOn = DateTime.Now,
                TotalProducts = 1
            };

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

            var product = new Product
            {
                Id = 0,
                Title = "La Primavera",
                ArtistId = 0,
                Description = "Description",
                Image = "C:/demo/folder",
                Price = 18000,
                QuantitySold = 0,
                AvgStars = 4.5,
                CreatedBy = "admin",
                ChangedBy = "admin",
                CreatedOn = DateTime.Now,
                ChangedOn = DateTime.Now
            };
            database.ProductManager.AddNewProduct(product);

            // Act
            database.ProductManager.RemoveProduct(product);

            // Assert
            var total = database.ProductManager.GetAllProduct().Count;
            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void Should_Update_Entity()
        {
            // Arrange
            var database = new DatabaseConnection(ConnectionType.InMemory);

            var user = new User
            {
                Id = 0,
                FirstName = "user",
                LastName = "user",
                Email = "email",
                City = "Buenos Aires",
                Country = "Argentina",
                SignupDate = DateTime.Now,
                OrderCount = 0,
                Password = new byte[0],
                CreatedBy = "admin",
                ChangedBy = "admin",
                CreatedOn = DateTime.Now,
                ChangedOn = DateTime.Now
            };
            database.UserManager.AddNewUser(user);
            user.Email = "user@spark.com";

            // Act
            database.UserManager.UpdateUser(user);

            // Assert
            var storedEmail = database.UserManager.FindUser(new Func<User, bool>( x => x.Id == 0)).FirstOrDefault().Email;
            Assert.AreEqual("user@spark.com", storedEmail);
        }
    }
}
