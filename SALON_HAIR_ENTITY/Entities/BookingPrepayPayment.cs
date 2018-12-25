using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class BookingPrepayPayment
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long BookingId { get; set; }
        public long BookingMethodId { get; set; }
        public decimal? Total { get; set; }
        public long? BookingBankingId { get; set; }
        public long? SalonId { get; set; }
        public long? SalonBranchId { get; set; }

        public Booking Booking { get; set; }
        public PaymentBanking BookingBanking { get; set; }
        public PaymentMethod BookingMethod { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
    }
}
