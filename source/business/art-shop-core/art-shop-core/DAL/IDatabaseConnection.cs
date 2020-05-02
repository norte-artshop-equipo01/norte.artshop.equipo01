using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace art_shop_core.DAL
{
    public interface IDatabaseConnection
    {
        bool TestConnection();
        void CloseConnection();
        void Add<T>(T item) where T : class;
        void Remove<T>(T item) where T : class;
        void Update<T>(T item) where T : class;
        List<T> Find<T>(Func<T, bool> filter) where T : class;
    }
}
