using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class CustomerDebtTransaction
    {
        public CustomerDebtTransaction()
        {
            CustomerDebtTransactionPayment = new HashSet<CustomerDebtTransactionPayment>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public long SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long SalonBranchId { get; set; }
        public long? CustomerId { get; set; }
        public string Action { get; set; }
        public decimal? Money { get; set; }
        public long? InvoiceId { get; set; }
        public long? CashBookTransactionId { get; set; }

        public CashBookTransaction CashBookTransaction { get; set; }
        public Customer Customer { get; set; }
        public Invoice Invoice { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public ICollection<CustomerDebtTransactionPayment> CustomerDebtTransactionPayment { get; set; }
    }
}
