//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Artshop.Data.Data.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart : BaseClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cart()
        {
            this.CartItem = new HashSet<CartItem>();
        }
    
        public int Id { get; set; }
        public string Cookie { get; set; }
        public System.DateTime CartDate { get; set; }
        public int ItemCount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItem> CartItem { get; set; }
    }
}
