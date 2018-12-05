using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class CurrencyUnit
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string Dest { get; set; }
        public decimal? Rate { get; set; }
        public string Country { get; set; }
        public long SalonId { get; set; }

        public Salon Salon { get; set; }
    }
}
