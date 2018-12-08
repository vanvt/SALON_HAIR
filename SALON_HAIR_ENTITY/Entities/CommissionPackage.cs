using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class CommissionPackage
    {
        public long StaffId { get; set; }
        public long PackageId { get; set; }
        public long SalonBranchId { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string CommissionUnit { get; set; }
        public float? CommissionValue { get; set; }
        public long Id { get; set; }

        public Package Package { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public Staff Staff { get; set; }
    }
}
