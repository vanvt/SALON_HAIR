﻿using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class Staff
    {
        public Staff()
        {
            BookingDetail = new HashSet<BookingDetail>();
            CommissionPackge = new HashSet<CommissionPackge>();
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
        public ICollection<BookingDetail> BookingDetail { get; set; }
        public ICollection<CommissionPackge> CommissionPackge { get; set; }
        public ICollection<CommissionService> CommissionService { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<InvoiceStaffArrangement> InvoiceStaffArrangement { get; set; }
        public ICollection<StaffSalonBranch> StaffSalonBranch { get; set; }
        public ICollection<StaffService> StaffService { get; set; }
    }
}
