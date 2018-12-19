﻿using System;
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
            CashBook = new HashSet<CashBook>();
            CashBookTransaction = new HashSet<CashBookTransaction>();
            CashBookTransactionDetail = new HashSet<CashBookTransactionDetail>();
            CurrencyUnit = new HashSet<CurrencyUnit>();
            Customer = new HashSet<Customer>();
            CustomerChannel = new HashSet<CustomerChannel>();
            CustomerPackage = new HashSet<CustomerPackage>();
            PaymentBanking = new HashSet<PaymentBanking>();
            PaymentMethod = new HashSet<PaymentMethod>();
            Product = new HashSet<Product>();
            ProductCategory = new HashSet<ProductCategory>();
            ProductSource = new HashSet<ProductSource>();
            ProductStatus = new HashSet<ProductStatus>();
            SalonBranch = new HashSet<SalonBranch>();
            Service = new HashSet<Service>();
            ServiceCategory = new HashSet<ServiceCategory>();
            SettingAdvance = new HashSet<SettingAdvance>();
            Staff = new HashSet<Staff>();
            StaffPackageCommissionTransaction = new HashSet<StaffPackageCommissionTransaction>();
            StaffProductCommissionTransaction = new HashSet<StaffProductCommissionTransaction>();
            StaffServiceCommissionTransaction = new HashSet<StaffServiceCommissionTransaction>();
            User = new HashSet<User>();
            Warehouse = new HashSet<Warehouse>();
            WarehouseTransaction = new HashSet<WarehouseTransaction>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Address { get; set; }
        public string Currency { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long? PhotoId { get; set; }

        public Photo Photo { get; set; }
        public ICollection<Authority> Authority { get; set; }
        public ICollection<Booking> Booking { get; set; }
        public ICollection<BookingLog> BookingLog { get; set; }
        public ICollection<CashBook> CashBook { get; set; }
        public ICollection<CashBookTransaction> CashBookTransaction { get; set; }
        public ICollection<CashBookTransactionDetail> CashBookTransactionDetail { get; set; }
        public ICollection<CurrencyUnit> CurrencyUnit { get; set; }
        public ICollection<Customer> Customer { get; set; }
        public ICollection<CustomerChannel> CustomerChannel { get; set; }
        public ICollection<CustomerPackage> CustomerPackage { get; set; }
        public ICollection<PaymentBanking> PaymentBanking { get; set; }
        public ICollection<PaymentMethod> PaymentMethod { get; set; }
        public ICollection<Product> Product { get; set; }
        public ICollection<ProductCategory> ProductCategory { get; set; }
        public ICollection<ProductSource> ProductSource { get; set; }
        public ICollection<ProductStatus> ProductStatus { get; set; }
        public ICollection<SalonBranch> SalonBranch { get; set; }
        public ICollection<Service> Service { get; set; }
        public ICollection<ServiceCategory> ServiceCategory { get; set; }
        public ICollection<SettingAdvance> SettingAdvance { get; set; }
        public ICollection<Staff> Staff { get; set; }
        public ICollection<StaffPackageCommissionTransaction> StaffPackageCommissionTransaction { get; set; }
        public ICollection<StaffProductCommissionTransaction> StaffProductCommissionTransaction { get; set; }
        public ICollection<StaffServiceCommissionTransaction> StaffServiceCommissionTransaction { get; set; }
        public ICollection<User> User { get; set; }
        public ICollection<Warehouse> Warehouse { get; set; }
        public ICollection<WarehouseTransaction> WarehouseTransaction { get; set; }
    }
}
