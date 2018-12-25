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
            BookingPrepayPayment = new HashSet<BookingPrepayPayment>();
            Invoice = new HashSet<Invoice>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public long SalonBranchId { get; set; }
        public long SalonId { get; set; }
        public long? CustomerId { get; set; }
        public DateTime? Date { get; set; }
        public int? NumberCustomer { get; set; }
        public string BookingStatus { get; set; }
        public string Note { get; set; }
        public bool? IsSameService { get; set; }
        public long? CustomerChannelId { get; set; }
        public long? SourceChannelId { get; set; }
        public string ColorCode { get; set; }
        public string BookingCode { get; set; }
        public string DateString { get; set; }
        public long? SelectedPackageId { get; set; }

        public Customer Customer { get; set; }
        public CustomerChannel CustomerChannel { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public Package SelectedPackage { get; set; }
        public CustomerSource SourceChannel { get; set; }
        public ICollection<BookingDetail> BookingDetail { get; set; }
        public ICollection<BookingLog> BookingLog { get; set; }
        public ICollection<BookingPrepayPayment> BookingPrepayPayment { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
    }
}
