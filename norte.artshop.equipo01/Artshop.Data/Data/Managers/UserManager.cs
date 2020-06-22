using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;

namespace Artshop.Data.Data.Managers
{
    public class UserManager
    {
        private readonly IDatabaseData _database;

        public UserManager(IDatabaseData database)
        {
            _database = database;
        }

        public void AddNewUser(User user)
        {
            _database.Add(user);
        }

        public void RemoveUser(User user)
        {
            _database.Remove(user);
        }

        public void UpdateUser(User user)
        {
            _database.Update(user);
        }

        public List<User> FindUser(Func<User, bool> user)
        {
            return _database.Find(user);
        }

        public List<User> GetAllUser()
        {
            return _database.GetAll<User>();
        }
    }
}
