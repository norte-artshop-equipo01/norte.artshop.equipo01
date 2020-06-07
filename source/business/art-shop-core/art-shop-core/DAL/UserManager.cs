using art_shop_core.DAL.Security;
using art_shop_core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace art_shop_core.DAL
{
    public class UserManager
    {
        private readonly IDatabaseConnection database;
        private User currentUser;

        public UserManager(IDatabaseConnection database, User user)
        {
            this.database = database;
            currentUser = user;

            var userResult = FindUser(admin => admin.FirstName == "admin").FirstOrDefault();
            if (userResult == null)
            {
                CreateNewAdmin();
            }
        }

        private void CreateNewAdmin()
        {
            currentUser = new User { FirstName = "admin", LastName = "admin" };
            AddNewUser("admin", "admin", "admin@spark.com", "admin");
        }

        public void LogIn(string username, string password)
        {
            var userResult = FindUser(user => user.FirstName == username).FirstOrDefault();

            if (userResult == null || !userResult.Password.SequenceEqual(EncryptionHelper.HashPassword(password)))
            {
                throw new Exception("User or Password is incorrect");
            }

            currentUser = userResult;
        }

        public void AddNewUser(string firstName, string lastName, string email, string password, string city = null, string country = null)
        {
            var newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                SignupDate = DateTime.Now,
                CreatedOn = DateTime.Now,
                ChangedOn = DateTime.Now,
                OrderCount = 0,
                CreatedBy = $"{ currentUser.LastName }, { currentUser.FirstName }",
                ChangedBy = $"{ currentUser.LastName }, { currentUser.FirstName }",
                City = city,
                Country = country,
                Password = EncryptionHelper.HashPassword(password)
            };
            database.Add(newUser);
        }

        public void RemoveUser(User user)
        {
            database.Remove(user);
        }

        public void UpdateUser(User user)
        {
            database.Update(user);
        }

        public List<User> FindUser(Func<User, bool> filter)
        {
            return database.Find(filter);
        }

        public List<User> GetAllUsers()
        {
            return database.GetAll<User>();
        }
    }
}
