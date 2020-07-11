using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artshop.Website.Models
{
    public class CheckoutItem
    {
        public string ProductName { get; set; }
        public string ArtistName { get; set; }
        public string Image { get; set; }
        public int Amount { get; set; }
        public double UnitPrice { get; set; }
    }
}