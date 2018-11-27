using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class Package
    {
        public Package()
        {
            ServicePackage = new HashSet<ServicePackage>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PriceText { get; set; }
        public long SalonBranchId { get; set; }
        public long SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public decimal? OriginalPrice { get; set; }

        public ICollection<ServicePackage> ServicePackage { get; set; }
    }
}
