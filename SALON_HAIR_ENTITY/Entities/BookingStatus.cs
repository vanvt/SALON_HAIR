using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class BookingStatus
    {
        public BookingStatus()
        {
            BookingLog = new HashSet<BookingLog>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<BookingLog> BookingLog { get; set; }
    }
}
