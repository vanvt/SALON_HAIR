using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class CashBookTransaction
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public long SalonBranchId { get; set; }
        public long SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Action { get; set; }
        public string Code { get; set; }
        public string Cashier { get; set; }
        public string Description { get; set; }
        public decimal? Money { get; set; }

        public Salon Salon { get; set; }
        public ProductSalonBranch SalonBranch { get; set; }
    }
}
