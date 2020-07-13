using Artshop.Data.Data.EntityFramework;
using Artshop.Data.Data.Managers;
using Artshop.Data.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Artshop.Data.Data
{
    public class DatabaseConnection
    {
        private readonly IDatabaseData database;
        
        public readonly ArtistManager ArtistManager;
        public readonly ProductManager ProductManager;
        public readonly UserManager UserManager;
        public readonly OrderManager OrderManager;
        public readonly CartManager CartManager;
        public readonly CartItemManager CartItemManager;
        public readonly OrderNumberManager OrderNumberManager;

        public DatabaseConnection(ConnectionType type, string connection = null)
        {
            database = DatabaseFactory.GetDatabase(type, connection);
            ArtistManager = new ArtistManager(DatabaseFactory.GetDatabase(type, connection));
            ProductManager = new ProductManager(DatabaseFactory.GetDatabase(type, connection));
            UserManager = new UserManager(DatabaseFactory.GetDatabase(type, connection));
            OrderManager = new OrderManager(DatabaseFactory.GetDatabase(type, connection));
            CartManager = new CartManager(DatabaseFactory.GetDatabase(type, connection));
            CartItemManager = new CartItemManager(DatabaseFactory.GetDatabase(type, connection));
            OrderNumberManager = new OrderNumberManager(DatabaseFactory.GetDatabase(type, connection));
        }

        public void RunCustomCommand(string command)
        {
            database.RunCustomCommand(command);
        }

        public void Logger(Exception exception, HttpContext context)
        {
            string userId = string.Empty;
            try { userId = context.User.Identity.Name; }
            catch {/* no hacer nada, o enviar un correo electrónico al webmaster */ }

            var error = new Error
            {
                UserId = userId,
                Exception = exception.GetType().FullName,
                Message = exception.Message,
                Everything = exception.ToString(),
                IpAddress = context.Request.UserHostAddress,
                UserAgent = context.Request.UserAgent,
                PathAndQuery = context.Request.Url == null ? string.Empty : context.Request.Url.PathAndQuery,
                HttpReferer = context.Request.UrlReferrer == null ? string.Empty : context.Request.UrlReferrer.PathAndQuery,
            };

            database.Add(error);
        }
        public List<ValidationResult> ValidateModel<T>(T model)
        {
            ValidationContext v = new ValidationContext(model);
            List<ValidationResult> r = new List<ValidationResult>();

            bool validate = Validator.TryValidateObject(model, v, r, true);

            if (validate)
            {
                return null;
            }
            else
            {
                return r;
            }
        }
    }
}
