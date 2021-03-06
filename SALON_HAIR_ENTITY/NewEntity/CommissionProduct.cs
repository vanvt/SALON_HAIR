﻿using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class CommissionProduct
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public long SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long? StaffBranchId { get; set; }
        public long? ProductBranchId { get; set; }
        public long? CommisonUnitId { get; set; }
        public float? CommisonValue { get; set; }

        public DiscountUnit CommisonUnit { get; set; }
        public ProductSalonBranch ProductBranch { get; set; }
        public StaffSalonBranch StaffBranch { get; set; }
    }
}
