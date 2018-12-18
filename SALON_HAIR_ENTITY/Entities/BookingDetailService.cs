using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class BookingDetailService
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public long BookingDetailId { get; set; }
        public long ServiceId { get; set; }
        public bool? IsPaid { get; set; }

        public BookingDetail BookingDetail { get; set; }
        public Service Service { get; set; }
    }
}
