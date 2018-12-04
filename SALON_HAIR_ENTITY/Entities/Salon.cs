using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class Salon
    {
        public Salon()
        {
            Authority = new HashSet<Authority>();
            Booking = new HashSet<Booking>();
            BookingLog = new HashSet<BookingLog>();
            CurrencyUnit = new HashSet<CurrencyUnit>();
            Customer = new HashSet<Customer>();
            CustomerChannel = new HashSet<CustomerChannel>();
            PaymentBanking = new HashSet<PaymentBanking>();
            PaymentMethod = new HashSet<PaymentMethod>();
            Product = new HashSet<Product>();
            ProductCategory = new HashSet<ProductCategory>();
            ProductSource = new HashSet<ProductSource>();
            ProductStatus = new HashSet<ProductStatus>();
            SalonBranch = new HashSet<SalonBranch>();
            Service = new HashSet<Service>();
            ServiceCategory = new HashSet<ServiceCategory>();
            Staff = new HashSet<Staff>();
            StaffGroup = new HashSet<StaffGroup>();
            User = new HashSet<User>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Address { get; set; }
        public byte[] Cover { get; set; }
        public string CoverContentType { get; set; }
        public string Currency { get; set; }
        public string Email { get; set; }
        public byte[] Logo { get; set; }
        public string LogoContentType { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string OpenHour { get; set; }
        public string SpaStatus { get; set; }
        public string WebSite { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<Authority> Authority { get; set; }
        public ICollection<Booking> Booking { get; set; }
        public ICollection<BookingLog> BookingLog { get; set; }
        public ICollection<CurrencyUnit> CurrencyUnit { get; set; }
        public ICollection<Customer> Customer { get; set; }
        public ICollection<CustomerChannel> CustomerChannel { get; set; }
        public ICollection<PaymentBanking> PaymentBanking { get; set; }
        public ICollection<PaymentMethod> PaymentMethod { get; set; }
        public ICollection<Product> Product { get; set; }
        public ICollection<ProductCategory> ProductCategory { get; set; }
        public ICollection<ProductSource> ProductSource { get; set; }
        public ICollection<ProductStatus> ProductStatus { get; set; }
        public ICollection<SalonBranch> SalonBranch { get; set; }
        public ICollection<Service> Service { get; set; }
        public ICollection<ServiceCategory> ServiceCategory { get; set; }
        public ICollection<Staff> Staff { get; set; }
        public ICollection<StaffGroup> StaffGroup { get; set; }
        public ICollection<User> User { get; set; }
    }
}
