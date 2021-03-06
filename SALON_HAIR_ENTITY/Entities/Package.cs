﻿using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class Package
    {
        public Package()
        {
            Booking = new HashSet<Booking>();
            CommissionPackage = new HashSet<CommissionPackage>();
            CustomerPackage = new HashSet<CustomerPackage>();
            PackageSalonBranch = new HashSet<PackageSalonBranch>();
            ServicePackage = new HashSet<ServicePackage>();
            StaffPackageCommissionTransaction = new HashSet<StaffPackageCommissionTransaction>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public decimal? OriginalPrice { get; set; }
        public int? NumberOfUse { get; set; }
        public int? UsedInMonth { get; set; }

        public ICollection<Booking> Booking { get; set; }
        public ICollection<CommissionPackage> CommissionPackage { get; set; }
        public ICollection<CustomerPackage> CustomerPackage { get; set; }
        public ICollection<PackageSalonBranch> PackageSalonBranch { get; set; }
        public ICollection<ServicePackage> ServicePackage { get; set; }
        public ICollection<StaffPackageCommissionTransaction> StaffPackageCommissionTransaction { get; set; }
    }
}
