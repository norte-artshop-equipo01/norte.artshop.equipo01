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
        public static IDatabaseData GetDatabase(ConnectionType type)
        {
            switch (type)
            {
                case ConnectionType.InMemory:
                    return new InMemoryData();
                case ConnectionType.Database:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
