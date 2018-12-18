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
            CashBook = new HashSet<CashBook>();
            CashBookTransaction = new HashSet<CashBookTransaction>();
            CommissionPackage = new HashSet<CommissionPackage>();
            CommissionProduct = new HashSet<CommissionProduct>();
            CommissionService = new HashSet<CommissionService>();
            Customer = new HashSet<Customer>();
            PackageSalonBranch = new HashSet<PackageSalonBranch>();
            Product = new HashSet<Product>();
            ProductSalonBranch = new HashSet<ProductSalonBranch>();
            Service = new HashSet<Service>();
            ServiceSalonBranch = new HashSet<ServiceSalonBranch>();
            StaffPackageCommissionTransaction = new HashSet<StaffPackageCommissionTransaction>();
            StaffProductCommissionTransaction = new HashSet<StaffProductCommissionTransaction>();
            StaffSalonBranch = new HashSet<StaffSalonBranch>();
            StaffServiceCommissionTransaction = new HashSet<StaffServiceCommissionTransaction>();
            User = new HashSet<User>();
            UserSalonBranch = new HashSet<UserSalonBranch>();
            Warehouse = new HashSet<Warehouse>();
            WarehouseTransaction = new HashSet<WarehouseTransaction>();
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
        public ICollection<CashBook> CashBook { get; set; }
        public ICollection<CashBookTransaction> CashBookTransaction { get; set; }
        public ICollection<CommissionPackage> CommissionPackage { get; set; }
        public ICollection<CommissionProduct> CommissionProduct { get; set; }
        public ICollection<CommissionService> CommissionService { get; set; }
        public ICollection<Customer> Customer { get; set; }
        public ICollection<PackageSalonBranch> PackageSalonBranch { get; set; }
        public ICollection<Product> Product { get; set; }
        public ICollection<ProductSalonBranch> ProductSalonBranch { get; set; }
        public ICollection<Service> Service { get; set; }
        public ICollection<ServiceSalonBranch> ServiceSalonBranch { get; set; }
        public ICollection<StaffPackageCommissionTransaction> StaffPackageCommissionTransaction { get; set; }
        public ICollection<StaffProductCommissionTransaction> StaffProductCommissionTransaction { get; set; }
        public ICollection<StaffSalonBranch> StaffSalonBranch { get; set; }
        public ICollection<StaffServiceCommissionTransaction> StaffServiceCommissionTransaction { get; set; }
        public ICollection<User> User { get; set; }
        public ICollection<UserSalonBranch> UserSalonBranch { get; set; }
        public ICollection<Warehouse> Warehouse { get; set; }
        public ICollection<WarehouseTransaction> WarehouseTransaction { get; set; }
    }
}
