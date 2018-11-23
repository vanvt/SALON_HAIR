using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class CustomerPackage
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long CustomerId { get; set; }
        public long PackageId { get; set; }
        public int? NumberUse { get; set; }
        public int? NumberRemaining { get; set; }
        public long? InoveId { get; set; }

        public Customer Customer { get; set; }
        public Invoice Inove { get; set; }
        public Package Package { get; set; }
    }
}
