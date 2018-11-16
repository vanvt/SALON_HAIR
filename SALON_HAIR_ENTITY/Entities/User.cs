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
        }

        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool Activated { get; set; }
        public string ActivationKey { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string LangKey { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? ResetDate { get; set; }
        public string ResetKey { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? SalonId { get; set; }
        public long? SalonBranchId { get; set; }
        public long? SalonBranchCurrentId { get; set; }

        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public SalonBranch SalonBranchCurrent { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<UserAuthority> UserAuthority { get; set; }
    }
}
