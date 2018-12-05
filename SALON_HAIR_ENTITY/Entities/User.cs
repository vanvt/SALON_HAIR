using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class User
    {
        public User()
        {
            Invoice = new HashSet<Invoice>();
            UserAuthority = new HashSet<UserAuthority>();
            UserSalonBranch = new HashSet<UserSalonBranch>();
        }

        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Status { get; set; }
        public long? SalonId { get; set; }
        public long? SalonBranchCurrentId { get; set; }
        public long? PhotoId { get; set; }

        public Photo Photo { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranchCurrent { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<UserAuthority> UserAuthority { get; set; }
        public ICollection<UserSalonBranch> UserSalonBranch { get; set; }
    }
}
