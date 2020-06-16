using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artshop.Tests
{
    internal static class TestUtils
    {
        internal static List<Artist> GetArtists()
        {
            return new List<Artist>
            {
                new Artist
                {
                    FirstName = "Sandro",
                    LastName = "Botticelli",
                    Country = "Argentina",
                    LifeSpan = "50",
                    Description = "Description",
                    CreatedBy = "admin",
                    ChangedBy = "admin",
                    CreatedOn = DateTime.Now,
                    ChangedOn = DateTime.Now
                },
                new Artist
                {
                    FirstName = "Leonardo",
                    LastName = "Da Vinci",
                    Country = "Italia",
                    LifeSpan = "50",
                    Description = "Description",
                    CreatedBy = "admin",
                    ChangedBy = "admin",
                    CreatedOn = DateTime.Now,
                    ChangedOn = DateTime.Now
                }
            };
        }

        internal static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Title = "La Primavera",
                    Description = "Description",
                    Image = "C:/demo/folder",
                    Price = 18000,
                    QuantitySold = 0,
                    AvgStars = 4.5,
                    CreatedBy = "admin",
                    ChangedBy = "admin",
                    CreatedOn = DateTime.Now,
                    ChangedOn = DateTime.Now
                },
                new Product
                {
                    Title = "La Fortaleza",
                    Description = "Description",
                    Image = "C:/demo/folder",
                    Price = 18000,
                    QuantitySold = 0,
                    AvgStars = 4.5,
                    CreatedBy = "admin",
                    ChangedBy = "admin",
                    CreatedOn = DateTime.Now,
                    ChangedOn = DateTime.Now
                }
            };
        }

        internal static List<Order>GetOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    OrderNumber = 3,
                    TotalPrice = 18000,
                    ItemCount = 10,
                    CreatedBy = "admin",
                    ChangedBy = "admin",
                    CreatedOn = DateTime.Now,
                    ChangedOn = DateTime.Now,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    OrderNumber = 4,
                    TotalPrice = 20000,
                    ItemCount = 10,
                    CreatedBy = "admin",
                    ChangedBy = "admin",
                    CreatedOn = DateTime.Now,
                    ChangedOn = DateTime.Now,
                    OrderDate = DateTime.Now
                }
            };
        }

        internal static List<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "user01",
                    LastName = "user01",
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
                },
                new User
                {
                    Id = 2,
                    FirstName = "user02",
                    LastName = "user02",
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
                }
            };
        }
    }
}
