using Artshop.Data.Data.EntityFramework;
using Artshop.Data.Data.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artshop.Data.Data
{
    public enum ConnectionType
    {
        InMemory,
        Database
    }

    internal static class DatabaseFactory
    {
        public static IDatabaseData GetDatabase(ConnectionType type, string connection = null)
        {
            switch (type)
            {
                case ConnectionType.InMemory:
                    return new InMemoryData();
                case ConnectionType.Database:
                    return new EntityFrameworkData(connection);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
