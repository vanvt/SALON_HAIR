using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class Staff
    {
        public Staff()
        {
            CommissionPackage = new HashSet<CommissionPackage>();
            CommissionProduct = new HashSet<CommissionProduct>();
            CommissionService = new HashSet<CommissionService>();
            Invoice = new HashSet<Invoice>();
            InvoiceStaffArrangement = new HashSet<InvoiceStaffArrangement>();
            StaffSalonBranch = new HashSet<StaffSalonBranch>();
            StaffService = new HashSet<StaffService>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Dob { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public long SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long? StaffGroupId { get; set; }

        public Salon Salon { get; set; }
        public StaffGroup StaffGroup { get; set; }
        public ICollection<CommissionPackage> CommissionPackage { get; set; }
        public ICollection<CommissionProduct> CommissionProduct { get; set; }
        public ICollection<CommissionService> CommissionService { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<InvoiceStaffArrangement> InvoiceStaffArrangement { get; set; }
        public ICollection<StaffSalonBranch> StaffSalonBranch { get; set; }
        public ICollection<StaffService> StaffService { get; set; }
    }
}
