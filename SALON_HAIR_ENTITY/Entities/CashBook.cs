using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class CashBook
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public long SalonBranchId { get; set; }
        public long SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public decimal? EarlyFund { get; set; }
        public decimal? EndFund { get; set; }
        public decimal? TotalExpenditure { get; set; }
        public decimal? TotalRevenue { get; set; }
        public int? Day { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }

        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
    }
}
