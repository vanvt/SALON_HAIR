using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.BackupEntities
{
    public partial class Commission
    {
        public Commission()
        {
            CommissionDetail = new HashSet<CommissionDetail>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public long SalonBranchId { get; set; }
        public long SalonId { get; set; }
        public long? ServiceCategoryId { get; set; }
        public float? RetailCommisionValue { get; set; }
        public long? RetailCommisionUnitId { get; set; }
        public float? WholesaleCommisionValue { get; set; }
        public long? WholesaleCommisionUnitId { get; set; }
        public float? LimitCommisionValue { get; set; }
        public long? LimitCommisionUnitId { get; set; }
        public long? StaffCommisonGroupId { get; set; }

        public CommissionUnit LimitCommisionUnit { get; set; }
        public CommissionUnit RetailCommisionUnit { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
        public StaffCommisonGroup StaffCommisonGroup { get; set; }
        public CommissionUnit WholesaleCommisionUnit { get; set; }
        public ICollection<CommissionDetail> CommissionDetail { get; set; }
    }
}
