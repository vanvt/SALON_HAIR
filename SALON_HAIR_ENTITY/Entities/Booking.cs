using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class Booking
    {
        public Booking()
        {
            BookingDetail = new HashSet<BookingDetail>();
            BookingLog = new HashSet<BookingLog>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string Name { get; set; }
        public long SalonBranchId { get; set; }
        public long SalonId { get; set; }
        public long? CustomerId { get; set; }
        public DateTime? Date { get; set; }
        public int? NumberCustomer { get; set; }
        public long? BookingStatusId { get; set; }
        public string Note { get; set; }

        public BookingStatus BookingStatus { get; set; }
        public Customer Customer { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public ICollection<BookingDetail> BookingDetail { get; set; }
        public ICollection<BookingLog> BookingLog { get; set; }
    }
}
