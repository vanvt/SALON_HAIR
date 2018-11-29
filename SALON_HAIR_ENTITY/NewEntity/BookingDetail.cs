using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class BookingDetail
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public long BookingId { get; set; }
        public long ServiceId { get; set; }
        public long? StaffId { get; set; }

        public Booking Booking { get; set; }
        public Service Service { get; set; }
        public Staff Staff { get; set; }
    }
}
