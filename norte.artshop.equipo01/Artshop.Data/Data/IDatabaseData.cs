using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Artshop.Data.Data
{
    public interface IDatabaseData
    {
        void Add<T>(T item) where T : class;
        void CloseConnection();
        List<T> Find<T>(Func<T, bool> filter) where T : class;
        List<T> GetAll<T>() where T : class;
        void Remove<T>(T item) where T : class;
        bool TestConnection();
        void Update<T>(T item) where T : class;
        void RunCustomCommand(string command);
        
    }
}
