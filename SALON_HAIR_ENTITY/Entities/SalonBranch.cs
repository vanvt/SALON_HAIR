using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class SalonBranch
    {
        public SalonBranch()
        {
            Booking = new HashSet<Booking>();
            BookingLog = new HashSet<BookingLog>();
            Customer = new HashSet<Customer>();
            Product = new HashSet<Product>();
            ProductSalonBranch = new HashSet<ProductSalonBranch>();
            Service = new HashSet<Service>();
            ServiceSalonBranch = new HashSet<ServiceSalonBranch>();
            StaffSalonBranch = new HashSet<StaffSalonBranch>();
            User = new HashSet<User>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string ActiveStep { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public float? Latitude { get; set; }
        public string Location { get; set; }
        public float? Longitude { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string OpenHour { get; set; }
        public string OpenHourFrom { get; set; }
        public int? OpenHourFromValue { get; set; }
        public int? OpenHourMinuteFromValue { get; set; }
        public int? OpenHourMinuteToValue { get; set; }
        public string OpenHourTo { get; set; }
        public int? OpenHourToValue { get; set; }
        public string SettingStep { get; set; }
        public long? SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public Salon Salon { get; set; }
        public ICollection<Booking> Booking { get; set; }
        public ICollection<BookingLog> BookingLog { get; set; }
        public ICollection<Customer> Customer { get; set; }
        public ICollection<Product> Product { get; set; }
        public ICollection<ProductSalonBranch> ProductSalonBranch { get; set; }
        public ICollection<Service> Service { get; set; }
        public ICollection<ServiceSalonBranch> ServiceSalonBranch { get; set; }
        public ICollection<StaffSalonBranch> StaffSalonBranch { get; set; }
        public ICollection<User> User { get; set; }
    }
}
