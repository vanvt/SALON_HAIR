using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class CommissionDetail
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public long? ServiceId { get; set; }
        public float? RetailCommisionValue { get; set; }
        public long RetailCommisionUnitId { get; set; }
        public float? WholesaleCommisionValue { get; set; }
        public long WholesaleCommisionUnitId { get; set; }
        public float? LimitCommisionValue { get; set; }
        public long LimitCommisionUnitId { get; set; }
        public long CommissionId { get; set; }

        public Commission Commission { get; set; }
        public CommissionUnit LimitCommisionUnit { get; set; }
        public CommissionUnit RetailCommisionUnit { get; set; }
        public Service Service { get; set; }
        public CommissionUnit WholesaleCommisionUnit { get; set; }
    }
}
