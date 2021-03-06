﻿using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.BackupEntities
{
    public partial class CommissionUnit
    {
        public CommissionUnit()
        {
            CommissionDetailLimitCommisionUnit = new HashSet<CommissionDetail>();
            CommissionDetailRetailCommisionUnit = new HashSet<CommissionDetail>();
            CommissionDetailWholesaleCommisionUnit = new HashSet<CommissionDetail>();
            CommissionLimitCommisionUnit = new HashSet<Commission>();
            CommissionRetailCommisionUnit = new HashSet<Commission>();
            CommissionWholesaleCommisionUnit = new HashSet<Commission>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string Name { get; set; }

        public ICollection<CommissionDetail> CommissionDetailLimitCommisionUnit { get; set; }
        public ICollection<CommissionDetail> CommissionDetailRetailCommisionUnit { get; set; }
        public ICollection<CommissionDetail> CommissionDetailWholesaleCommisionUnit { get; set; }
        public ICollection<Commission> CommissionLimitCommisionUnit { get; set; }
        public ICollection<Commission> CommissionRetailCommisionUnit { get; set; }
        public ICollection<Commission> CommissionWholesaleCommisionUnit { get; set; }
    }
}
