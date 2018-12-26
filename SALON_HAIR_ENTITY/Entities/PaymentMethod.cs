using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            BookingPrepayPayment = new HashSet<BookingPrepayPayment>();
            CashBook = new HashSet<CashBook>();
            CashBookTransaction = new HashSet<CashBookTransaction>();
            CustomerDebtTransactionPayment = new HashSet<CustomerDebtTransactionPayment>();
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
        public string Code { get; set; }

        public Salon Salon { get; set; }
        public ICollection<BookingPrepayPayment> BookingPrepayPayment { get; set; }
        public ICollection<CashBook> CashBook { get; set; }
        public ICollection<CashBookTransaction> CashBookTransaction { get; set; }
        public ICollection<CustomerDebtTransactionPayment> CustomerDebtTransactionPayment { get; set; }
        public ICollection<InvoicePayment> InvoicePayment { get; set; }
        public ICollection<PaymentBankingMethod> PaymentBankingMethod { get; set; }
    }
}
