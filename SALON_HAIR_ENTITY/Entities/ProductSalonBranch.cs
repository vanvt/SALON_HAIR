﻿using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class ProductSalonBranch
    {
        public ProductSalonBranch()
        {
            CashBookTransaction = new HashSet<CashBookTransaction>();
        }

        public long Id { get; set; }
        public long ProductId { get; set; }
        public long SalonBranchId { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public Product Product { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public ICollection<CashBookTransaction> CashBookTransaction { get; set; }
    }
}
