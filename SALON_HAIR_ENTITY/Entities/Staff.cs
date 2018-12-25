using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class Staff
    {
        public Staff()
        {
            CashBookTransaction = new HashSet<CashBookTransaction>();
            CashBookTransactionDetail = new HashSet<CashBookTransactionDetail>();
            CommissionArrangementSaleStaff = new HashSet<CommissionArrangement>();
            CommissionArrangementServiceStaff = new HashSet<CommissionArrangement>();
            CommissionPackage = new HashSet<CommissionPackage>();
            CommissionProduct = new HashSet<CommissionProduct>();
            CommissionService = new HashSet<CommissionService>();
            Invoice = new HashSet<Invoice>();
            InvoiceStaffArrangement = new HashSet<InvoiceStaffArrangement>();
            StaffPackageCommissionTransaction = new HashSet<StaffPackageCommissionTransaction>();
            StaffProductCommissionTransaction = new HashSet<StaffProductCommissionTransaction>();
            StaffSalonBranch = new HashSet<StaffSalonBranch>();
            StaffService = new HashSet<StaffService>();
            StaffServiceCommissionTransaction = new HashSet<StaffServiceCommissionTransaction>();
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
        public ICollection<CashBookTransaction> CashBookTransaction { get; set; }
        public ICollection<CashBookTransactionDetail> CashBookTransactionDetail { get; set; }
        public ICollection<CommissionArrangement> CommissionArrangementSaleStaff { get; set; }
        public ICollection<CommissionArrangement> CommissionArrangementServiceStaff { get; set; }
        public ICollection<CommissionPackage> CommissionPackage { get; set; }
        public ICollection<CommissionProduct> CommissionProduct { get; set; }
        public ICollection<CommissionService> CommissionService { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<InvoiceStaffArrangement> InvoiceStaffArrangement { get; set; }
        public ICollection<StaffPackageCommissionTransaction> StaffPackageCommissionTransaction { get; set; }
        public ICollection<StaffProductCommissionTransaction> StaffProductCommissionTransaction { get; set; }
        public ICollection<StaffSalonBranch> StaffSalonBranch { get; set; }
        public ICollection<StaffService> StaffService { get; set; }
        public ICollection<StaffServiceCommissionTransaction> StaffServiceCommissionTransaction { get; set; }
    }
}
