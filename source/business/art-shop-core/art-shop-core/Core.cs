using art_shop_core.DAL;
using art_shop_core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace art_shop_core
{
    public class Core
    {
        private readonly User currentUser;
        private readonly IDatabaseConnection database;
        public readonly UserManager UserManager;
        public readonly ProductManager ProductManager;
        public readonly ArtistManager ArtistManager;

        public Core(IDatabaseConnection databaseConnection)
        {
            database = databaseConnection;
            database.TestConnection();

            UserManager = new UserManager(databaseConnection, currentUser);
            ProductManager = new ProductManager(databaseConnection);
            ArtistManager = new ArtistManager(databaseConnection);
        }

        public void CloseDatabaseConnection()
        {
            database.CloseConnection();
        }
    }
}
