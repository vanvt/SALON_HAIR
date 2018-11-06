using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.BackupEntities
{
    public partial class Staff
    {
        public Staff()
        {
            BookingDetail = new HashSet<BookingDetail>();
            StaffService = new HashSet<StaffService>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Dob { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public long SalonBranchId { get; set; }
        public long SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long? StaffCommisionGroupId { get; set; }
        public long? PhotoId { get; set; }
        public long? StaffTitleId { get; set; }
        public bool? IsCasual { get; set; }
        public bool? IsWorkAllService { get; set; }

        public Photo Photo { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public StaffCommisonGroup StaffCommisionGroup { get; set; }
        public StaffTitle StaffTitle { get; set; }
        public ICollection<BookingDetail> BookingDetail { get; set; }
        public ICollection<StaffService> StaffService { get; set; }
    }
}
