﻿using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class ProductCategory
    {
        public ProductCategory()
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
