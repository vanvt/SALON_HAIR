using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class InvoicePayment
    {
        public InvoicePayment()
        {
            InvoicePaymentDetail = new HashSet<InvoicePaymentDetail>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long InvoiceId { get; set; }
        public string Note { get; set; }

        public Invoice Invoice { get; set; }
        public ICollection<InvoicePaymentDetail> InvoicePaymentDetail { get; set; }
    }
}
