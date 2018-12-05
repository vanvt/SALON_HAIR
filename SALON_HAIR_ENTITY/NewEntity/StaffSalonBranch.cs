using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class StaffSalonBranch
    {
        public StaffSalonBranch()
        {
            CommissionPackge = new HashSet<CommissionPackge>();
            CommissionProduct = new HashSet<CommissionProduct>();
            CommissionService = new HashSet<CommissionService>();
        }

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
        public ICollection<CommissionPackge> CommissionPackge { get; set; }
        public ICollection<CommissionProduct> CommissionProduct { get; set; }
        public ICollection<CommissionService> CommissionService { get; set; }
    }
}
