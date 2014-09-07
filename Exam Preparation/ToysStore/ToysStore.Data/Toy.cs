//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToysStore.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Toy
    {
        public Toy()
        {
            this.Categories = new HashSet<Category>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int ManufacturerId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Color { get; set; }
        public Nullable<int> AgeRangeId { get; set; }
    
        public virtual AgeRanx AgeRanx { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
