using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class CustomerPackage
    {
        public CustomerPackage()
        {
            InvoiceDetail = new HashSet<InvoiceDetail>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public long SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long? PackageId { get; set; }
        public uint? NumberOfPaid { get; set; }
        public uint? NumberOfUsed { get; set; }
        public int? NumberOfRemaining { get; set; }
        public long? CustomerId { get; set; }
        public int? Day { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public DateTime? ExpireDate { get; set; }

        public Customer Customer { get; set; }
        public Package Package { get; set; }
        public Salon Salon { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetail { get; set; }
    }
}
