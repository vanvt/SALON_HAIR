using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class BookingCustomerService
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public long BookingCutomerId { get; set; }
        public long ServiceId { get; set; }
        public string NoteStatus { get; set; }
        public string Note { get; set; }

        public BookingCustomer BookingCutomer { get; set; }
        public Service Service { get; set; }
    }
}
