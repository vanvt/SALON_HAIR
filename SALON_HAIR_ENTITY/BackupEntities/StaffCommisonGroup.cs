using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.BackupEntities
{
    public partial class StaffCommisonGroup
    {
        public StaffCommisonGroup()
        {
            Commission = new HashSet<Commission>();
            Staff = new HashSet<Staff>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Name { get; set; }
        public long SalonBranchId { get; set; }
        public long SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long? PhotoId { get; set; }
        public long? StaffTitleId { get; set; }

        public Photo Photo { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public StaffTitle StaffTitle { get; set; }
        public ICollection<Commission> Commission { get; set; }
        public ICollection<Staff> Staff { get; set; }
    }
}
