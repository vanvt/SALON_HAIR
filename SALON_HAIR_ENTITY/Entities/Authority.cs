using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class Authority
    {
        public Authority()
        {
            AuthorityRouter = new HashSet<AuthorityRouter>();
            UserAuthority = new HashSet<UserAuthority>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<AuthorityRouter> AuthorityRouter { get; set; }
        public ICollection<UserAuthority> UserAuthority { get; set; }
    }
}
