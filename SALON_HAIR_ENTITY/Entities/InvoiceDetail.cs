using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class InvoiceDetail
    {
        public InvoiceDetail()
        {
            CommissionArrangement = new HashSet<CommissionArrangement>();
            InvoiceStaffArrangement = new HashSet<InvoiceStaffArrangement>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long? InvoiceId { get; set; }
        public long? ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string ObjectName { get; set; }
        public string ObjectCode { get; set; }
        public decimal ObjectPrice { get; set; }
        public string DiscountUnit { get; set; }
        public long? DiscountValue { get; set; }
        public decimal? Total { get; set; }
        public int? Quantity { get; set; }
        public bool? IsPaid { get; set; }
        public long? SalonId { get; set; }
        public long? SalonBranchId { get; set; }
        public decimal? TotalIncludeDiscount { get; set; }
        public decimal? TotalExcludeDiscount { get; set; }
        public long? CustomerPackageId { get; set; }

        public CustomerPackage CustomerPackage { get; set; }
        public Invoice Invoice { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public ICollection<CommissionArrangement> CommissionArrangement { get; set; }
        public ICollection<InvoiceStaffArrangement> InvoiceStaffArrangement { get; set; }
    }
}
