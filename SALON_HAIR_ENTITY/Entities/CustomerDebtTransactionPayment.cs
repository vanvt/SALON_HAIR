using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class CustomerDebtTransactionPayment
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long? PaymentMethodId { get; set; }
        public decimal? Total { get; set; }
        public long? PaymentBankingId { get; set; }
        public long? SalonId { get; set; }
        public long? SalonBranchId { get; set; }
        public long? CustomerDebtTransactionId { get; set; }

        public CustomerDebtTransaction CustomerDebtTransaction { get; set; }
        public PaymentBanking PaymentBanking { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
    }
}
