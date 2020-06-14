using Artshop.Data.Data.Managers;
using Artshop.Data.Managers;

namespace Artshop.Data.Data
{
    public class DatabaseConnection
    {
        public readonly ArtistManager ArtistManager;
        public readonly ProductManager ProductManager;
        public readonly UserManager UserManager;
        public readonly OrderManager OrderManager;

        public DatabaseConnection(ConnectionType test)
        {
            ArtistManager = new ArtistManager(DatabaseFactory.GetDatabase(test));
            ProductManager = new ProductManager(DatabaseFactory.GetDatabase(test));
            UserManager = new UserManager(DatabaseFactory.GetDatabase(test));
            OrderManager = new OrderManager(DatabaseFactory.GetDatabase(test));
        }
    }
}
