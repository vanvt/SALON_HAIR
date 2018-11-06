using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.BackupEntities
{
    public partial class Salon
    {
        public Salon()
        {
            Booking = new HashSet<Booking>();
            BookingLog = new HashSet<BookingLog>();
            Commission = new HashSet<Commission>();
            Customer = new HashSet<Customer>();
            Product = new HashSet<Product>();
            SalonBranch = new HashSet<SalonBranch>();
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

        public ICollection<Booking> Booking { get; set; }
        public ICollection<BookingLog> BookingLog { get; set; }
        public ICollection<Commission> Commission { get; set; }
        public ICollection<Customer> Customer { get; set; }
        public ICollection<Product> Product { get; set; }
        public ICollection<SalonBranch> SalonBranch { get; set; }
        public ICollection<Service> Service { get; set; }
        public ICollection<Staff> Staff { get; set; }
        public ICollection<StaffCommisonGroup> StaffCommisonGroup { get; set; }
        public ICollection<StaffTitle> StaffTitle { get; set; }
        public ICollection<User> User { get; set; }
    }
}
