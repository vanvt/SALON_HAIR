using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class PaymentBankingMethod
    {
        public long BankingId { get; set; }
        public long PaymentMethodId { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public PaymentBanking Banking { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
