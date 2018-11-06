using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class Service
    {
        public Service()
        {
            BookingDetail = new HashSet<BookingDetail>();
            CommissionDetail = new HashSet<CommissionDetail>();
            ServicePackage = new HashSet<ServicePackage>();
            ServiceProduct = new HashSet<ServiceProduct>();
            StaffService = new HashSet<StaffService>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long? SalonBranchId { get; set; }
        public long SalonId { get; set; }
        public string Time { get; set; }
        public int? TimeValue { get; set; }
        public long? ServiceCategoryId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
        public ICollection<BookingDetail> BookingDetail { get; set; }
        public ICollection<CommissionDetail> CommissionDetail { get; set; }
        public ICollection<ServicePackage> ServicePackage { get; set; }
        public ICollection<ServiceProduct> ServiceProduct { get; set; }
        public ICollection<StaffService> StaffService { get; set; }
    }
}
