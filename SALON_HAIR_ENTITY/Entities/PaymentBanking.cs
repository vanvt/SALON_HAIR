using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class PaymentBanking
    {
        public PaymentBanking()
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
        public long? SalonId { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string BankHolder { get; set; }

        public Salon Salon { get; set; }
        public ICollection<InvoicePayment> InvoicePayment { get; set; }
        public ICollection<PaymentBankingMethod> PaymentBankingMethod { get; set; }
    }
}
