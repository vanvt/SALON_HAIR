using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class UserSalonBranch
    {
        public long UserId { get; set; }
        public long SpaBranchId { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }

        public SalonBranch SpaBranch { get; set; }
        public User User { get; set; }
    }
}
