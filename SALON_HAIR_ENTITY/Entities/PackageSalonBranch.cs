using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class PackageSalonBranch
    {
        public PackageSalonBranch()
        {
            CommissionPackge = new HashSet<CommissionPackge>();
        }

        public long Id { get; set; }
        public long PackageId { get; set; }
        public long SalonId { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<CommissionPackge> CommissionPackge { get; set; }
    }
}
