using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.BackupEntities
{
    public partial class StaffTitle
    {
        public StaffTitle()
        {
            Staff = new HashSet<Staff>();
            StaffCommisonGroup = new HashSet<StaffCommisonGroup>();
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

        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public ICollection<Staff> Staff { get; set; }
        public ICollection<StaffCommisonGroup> StaffCommisonGroup { get; set; }
    }
}
