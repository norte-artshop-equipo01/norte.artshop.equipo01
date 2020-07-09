using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artshop.Data.Data.EntityFramework
{
    public abstract class BaseClass
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedOn { get; set; }

        public BaseClass()
        {
            CreatedOn = DateTime.Now;
            ChangedOn = DateTime.Now;
        }
    }
}
