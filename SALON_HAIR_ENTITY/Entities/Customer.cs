﻿using System;
using System.Collections.Generic;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Booking = new HashSet<Booking>();
            BookingLog = new HashSet<BookingLog>();
            CashBookTransaction = new HashSet<CashBookTransaction>();
            CustomerDebtTransaction = new HashSet<CustomerDebtTransaction>();
            CustomerPackage = new HashSet<CustomerPackage>();
            Invoice = new HashSet<Invoice>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public DateTime? Updated { get; set; }
        public string Address { get; set; }
        public DateTime? Dob { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Sex { get; set; }
        public long SalonBranchId { get; set; }
        public long SalonId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public long? PhotoId { get; set; }
        public long? SoucreCustomerId { get; set; }
        public long? ChannelCustomerId { get; set; }
        public string Code { get; set; }

        public CustomerChannel ChannelCustomer { get; set; }
        public Photo Photo { get; set; }
        public Salon Salon { get; set; }
        public SalonBranch SalonBranch { get; set; }
        public CustomerSource SoucreCustomer { get; set; }
        public ICollection<Booking> Booking { get; set; }
        public ICollection<BookingLog> BookingLog { get; set; }
        public ICollection<CashBookTransaction> CashBookTransaction { get; set; }
        public ICollection<CustomerDebtTransaction> CustomerDebtTransaction { get; set; }
        public ICollection<CustomerPackage> CustomerPackage { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
    }
}
