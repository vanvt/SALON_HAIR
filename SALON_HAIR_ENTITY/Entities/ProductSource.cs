using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class ProductSource
    {
        public ProductSource()
        {
            Product = new HashSet<Product>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public long SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public Salon Salon { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
