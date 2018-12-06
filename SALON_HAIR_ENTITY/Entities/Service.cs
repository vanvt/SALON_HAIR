using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class Service
    {
        public Service()
        {
            BookingDetail = new HashSet<BookingDetail>();
            CommissionService = new HashSet<CommissionService>();
            InvoiceStaffArrangement = new HashSet<InvoiceStaffArrangement>();
            ServicePackage = new HashSet<ServicePackage>();
            ServiceProduct = new HashSet<ServiceProduct>();
            ServiceSalonBranch = new HashSet<ServiceSalonBranch>();
            StaffService = new HashSet<StaffService>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long? SalonBranchCreateId { get; set; }
        public long SalonId { get; set; }
        public string Time { get; set; }
        public int? TimeValue { get; set; }
        public long? ServiceCategoryId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public Salon Salon { get; set; }
        public SalonBranch SalonBranchCreate { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
        public ICollection<BookingDetail> BookingDetail { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<CommissionService> CommissionService { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<InvoiceStaffArrangement> InvoiceStaffArrangement { get; set; }
        public ICollection<ServicePackage> ServicePackage { get; set; }
        public ICollection<ServiceProduct> ServiceProduct { get; set; }
        public ICollection<ServiceSalonBranch> ServiceSalonBranch { get; set; }
        public ICollection<StaffService> StaffService { get; set; }
    }
}
