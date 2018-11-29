using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.NewEntity
{
    public partial class BookingLog
    {
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
        public long? BookingId { get; set; }

        public Booking Booking { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public Customer Customer { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
    }
}
