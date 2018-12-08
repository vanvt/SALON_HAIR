using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class CommissionService
    {
        public long Id { get; set; }
        public long StaffId { get; set; }
        public long ServiceId { get; set; }
        public long SalonBranchId { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string CommissionUnit { get; set; }
        public decimal? CommissionValue { get; set; }

        public SalonBranch SalonBranch { get; set; }
        public Service Service { get; set; }
        public Staff Staff { get; set; }
    }
}
