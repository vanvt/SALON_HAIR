using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class ProductUnit
    {
        public ProductUnit()
        {
            Product = new HashSet<Product>();
        }

        public long Id { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
