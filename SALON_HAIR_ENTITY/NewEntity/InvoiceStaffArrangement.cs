using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class InvoiceStaffArrangement
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long? InvoiceDetailId { get; set; }
        public long? InvoiceId { get; set; }
        public long? ServiceId { get; set; }
        public long? StaffId { get; set; }

        public Invoice Invoice { get; set; }
        public InvoiceDetail InvoiceDetail { get; set; }
        public Service Service { get; set; }
        public Staff Staff { get; set; }
    }
}
