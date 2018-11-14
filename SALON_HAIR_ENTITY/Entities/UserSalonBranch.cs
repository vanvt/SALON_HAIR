﻿using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class UserSalonBranch
    {
        public long UserId { get; set; }
        public long SpaId { get; set; }
        public long SpaBranchId { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
