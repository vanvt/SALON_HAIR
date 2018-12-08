using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class WarehouseTransactionDetail
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long WarehouseTransactionId { get; set; }
        public int? Quantity { get; set; }
        public long? ProductId { get; set; }
        public decimal? TotalVolume { get; set; }

        public Product Product { get; set; }
        public WarehouseTransaction WarehouseTransaction { get; set; }
    }
}
