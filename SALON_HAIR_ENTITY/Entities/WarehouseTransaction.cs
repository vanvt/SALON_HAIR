using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class WarehouseTransaction
    {
        public WarehouseTransaction()
        {
            WarehouseTransactionDetail = new HashSet<WarehouseTransactionDetail>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Creator { get; set; }
        public long? SalonBranchId { get; set; }
        public string Action { get; set; }

        public SalonBranch SalonBranch { get; set; }
        public ICollection<WarehouseTransactionDetail> WarehouseTransactionDetail { get; set; }
    }
}
