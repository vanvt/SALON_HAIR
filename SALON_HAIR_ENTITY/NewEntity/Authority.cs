﻿using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
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
        public string Status { get; set; }
        public long? SalonId { get; set; }

        public Salon Salon { get; set; }
        public ICollection<AuthorityRouter> AuthorityRouter { get; set; }
        public ICollection<UserAuthority> UserAuthority { get; set; }
    }
}
