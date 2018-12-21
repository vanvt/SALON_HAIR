using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class CommissionArrangement
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long InvoiceId { get; set; }
        public long? SaleStaffId { get; set; }
        public long? ServiceStaffId { get; set; }
        public long? ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string ObjectName { get; set; }
        public string ObjectCode { get; set; }
        public decimal ObjectPrice { get; set; }
        public decimal? ObjectPriceDiscount { get; set; }
        public bool? IsPaid { get; set; }
        public long? InvoiceDetailId { get; set; }
        public long? SalonId { get; set; }
        public long? SalonBranchId { get; set; }

        public Invoice Invoice { get; set; }
        public InvoiceDetail InvoiceDetail { get; set; }
        public Staff SaleStaff { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public Staff ServiceStaff { get; set; }
    }
}
