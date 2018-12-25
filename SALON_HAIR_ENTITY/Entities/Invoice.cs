using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class Invoice
    {
        public Invoice()
        {
            CashBookTransaction = new HashSet<CashBookTransaction>();
            CommissionArrangement = new HashSet<CommissionArrangement>();
            CustomerDebtTransaction = new HashSet<CustomerDebtTransaction>();
            InvoiceDetail = new HashSet<InvoiceDetail>();
            InvoicePayment = new HashSet<InvoicePayment>();
            InvoiceStaffArrangement = new HashSet<InvoiceStaffArrangement>();
            WarehouseTransaction = new HashSet<WarehouseTransaction>();
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
        public string DiscountUnit { get; set; }
        public decimal DiscountValue { get; set; }
        public long? CashierId { get; set; }
        public long? SalesmanId { get; set; }
        public bool? IsDisplay { get; set; }
        public string Note { get; set; }
        public decimal? Total { get; set; }
        public string NotePayment { get; set; }
        public string NoteArrangement { get; set; }
        public decimal? CashBack { get; set; }
        public decimal TotalDetails { get; set; }
        public string PaymentStatus { get; set; }
        public long? BookingId { get; set; }
        public decimal Prepay { get; set; }

        public Booking Booking { get; set; }
        public User Cashier { get; set; }
        public Customer Customer { get; set; }
        public Staff Salesman { get; set; }
        public ICollection<CashBookTransaction> CashBookTransaction { get; set; }
        public ICollection<CommissionArrangement> CommissionArrangement { get; set; }
        public ICollection<CustomerDebtTransaction> CustomerDebtTransaction { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetail { get; set; }
        public ICollection<InvoicePayment> InvoicePayment { get; set; }
        public ICollection<InvoiceStaffArrangement> InvoiceStaffArrangement { get; set; }
        public ICollection<WarehouseTransaction> WarehouseTransaction { get; set; }
    }
}
