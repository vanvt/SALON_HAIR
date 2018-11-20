using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class InvoicePaymentDetail
    {
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long InvoicePaymentId { get; set; }
        public long InvoiceMethodId { get; set; }
        public decimal? Total { get; set; }

        public PaymentMethod InvoiceMethod { get; set; }
        public InvoicePayment InvoicePayment { get; set; }
    }
}
