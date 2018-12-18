using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class StaffProductCommissionTransaction
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public decimal CommissionValue { get; set; }
        public long? SalonId { get; set; }
        public long? SalonBranchId { get; set; }
        public long? StaffId { get; set; }
        public long? ProductId { get; set; }

        public Product Product { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public Staff Staff { get; set; }
    }
}
