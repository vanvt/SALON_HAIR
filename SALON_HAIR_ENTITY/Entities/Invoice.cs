using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceDetail = new HashSet<InvoiceDetail>();
            InvoiceStaffArrangement = new HashSet<InvoiceStaffArrangement>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Code { get; set; }
        public long SalonBranchId { get; set; }
        public long SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long? CustomerId { get; set; }
        public long InvoiceStatusId { get; set; }
        public long? DiscountUnitId { get; set; }
        public float? DiscountUnitValue { get; set; }
        public long? CashierId { get; set; }
        public long? SalesmanId { get; set; }
        public bool? IsDisplay { get; set; }
        public string Note { get; set; }

        public User Cashier { get; set; }
        public Customer Customer { get; set; }
        public CommissionUnit DiscountUnit { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public Staff Salesman { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetail { get; set; }
        public ICollection<InvoiceStaffArrangement> InvoiceStaffArrangement { get; set; }
    }
}
