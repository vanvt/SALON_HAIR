using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.BackupEntities
{
    public partial class SalonBranch
    {
        public SalonBranch()
        {
            Booking = new HashSet<Booking>();
            BookingLog = new HashSet<BookingLog>();
            Commission = new HashSet<Commission>();
            Customer = new HashSet<Customer>();
            Product = new HashSet<Product>();
            Service = new HashSet<Service>();
            Staff = new HashSet<Staff>();
            StaffCommisonGroup = new HashSet<StaffCommisonGroup>();
            StaffTitle = new HashSet<StaffTitle>();
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
        public ICollection<Commission> Commission { get; set; }
        public ICollection<Customer> Customer { get; set; }
        public ICollection<Product> Product { get; set; }
        public ICollection<Service> Service { get; set; }
        public ICollection<Staff> Staff { get; set; }
        public ICollection<StaffCommisonGroup> StaffCommisonGroup { get; set; }
        public ICollection<StaffTitle> StaffTitle { get; set; }
        public ICollection<User> User { get; set; }
    }
}
