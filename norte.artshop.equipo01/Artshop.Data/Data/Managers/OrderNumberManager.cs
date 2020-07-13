using Artshop.Data.Data;
using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Artshop.Data.Data.Managers
{
    public class OrderNumberManager
    {
        private readonly IDatabaseData _database;

        public OrderNumberManager(IDatabaseData database)
        {
            _database = database;
        }

        public void AddNewOrderNumber(OrderNumber ordernumber)
        {
            _database.Add(ordernumber);
        }
    }
}
