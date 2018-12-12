using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class BookingDetail
    {
        public BookingDetail()
        {
            BookingDetailService = new HashSet<BookingDetailService>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public long BookingId { get; set; }
        public bool? NoteStatus { get; set; }
        public string Note { get; set; }

        public Booking Booking { get; set; }
        public ICollection<BookingDetailService> BookingDetailService { get; set; }
    }
}
