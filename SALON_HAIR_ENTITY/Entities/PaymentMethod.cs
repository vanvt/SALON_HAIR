using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            InvoicePayment = new HashSet<InvoicePayment>();
            PaymentBankingMethod = new HashSet<PaymentBankingMethod>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Name { get; set; }
        public long? SalonId { get; set; }

        public Salon Salon { get; set; }
        public ICollection<InvoicePayment> InvoicePayment { get; set; }
        public ICollection<PaymentBankingMethod> PaymentBankingMethod { get; set; }
    }
}
