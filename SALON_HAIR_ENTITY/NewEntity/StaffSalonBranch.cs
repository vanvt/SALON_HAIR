using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class StaffSalonBranch
    {
        public long Id { get; set; }
        public long StaffId { get; set; }
        public long SalonBranchId { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public SalonBranch SalonBranch { get; set; }
        public Staff Staff { get; set; }
    }
}
