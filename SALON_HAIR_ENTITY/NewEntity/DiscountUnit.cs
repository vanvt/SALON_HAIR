﻿using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class DiscountUnit
    {
        public DiscountUnit()
        {
            CommissionPackge = new HashSet<CommissionPackge>();
            CommissionService = new HashSet<CommissionService>();
            Invoice = new HashSet<Invoice>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<CommissionPackge> CommissionPackge { get; set; }
        public ICollection<CommissionService> CommissionService { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
    }
}
