using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class InvoicePayment
    {
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long InvoiceId { get; set; }
        public long InvoiceMethodId { get; set; }
        public decimal? Total { get; set; }
        public long? InvoiceBankingId { get; set; }

        public Invoice Invoice { get; set; }
        public PaymentBanking InvoiceBanking { get; set; }
        public PaymentMethod InvoiceMethod { get; set; }
    }
}
