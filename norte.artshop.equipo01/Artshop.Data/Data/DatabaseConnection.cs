using Artshop.Data.Data.Managers;
using Artshop.Data.Managers;

namespace Artshop.Data.Data
{
    public class DatabaseConnection
    {
        private readonly IDatabaseData database;
        
        public readonly ArtistManager ArtistManager;
        public readonly ProductManager ProductManager;
        public readonly UserManager UserManager;
        public readonly OrderManager OrderManager;

        public DatabaseConnection(ConnectionType type, string connection = null)
        {
            database = DatabaseFactory.GetDatabase(type, connection);
            ArtistManager = new ArtistManager(DatabaseFactory.GetDatabase(type, connection));
            ProductManager = new ProductManager(DatabaseFactory.GetDatabase(type, connection));
            UserManager = new UserManager(DatabaseFactory.GetDatabase(type, connection));
            OrderManager = new OrderManager(DatabaseFactory.GetDatabase(type, connection));
        }

        public void RunCustomCommand(string command)
        {
            database.RunCustomCommand(command);
        }
    }
}
