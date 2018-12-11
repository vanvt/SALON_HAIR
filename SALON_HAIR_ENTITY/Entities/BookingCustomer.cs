using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class BookingCustomer
    {
        public BookingCustomer()
        {
            BookingCustomerService = new HashSet<BookingCustomerService>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public long BookingId { get; set; }
        public long CutomerId { get; set; }
        public bool? NoteStatus { get; set; }
        public string Note { get; set; }

        public Booking Booking { get; set; }
        public Customer Cutomer { get; set; }
        public ICollection<BookingCustomerService> BookingCustomerService { get; set; }
    }
}
