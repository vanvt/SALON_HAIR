using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SALON_HAIR_ENTITY.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SALON_HAIR_ENTITY.Extensions
{
    public static class GlobalQueryFillter
    {
        public static ModelBuilder BuilCustomFillter( ModelBuilder  builder)
        {

            //builder.Entity<Authority>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<AuthorityRouter>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            //builder.Entity<Photo>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Product>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ProductUnit>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ProductCategory>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            //builder.Entity<Status>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            //builder.Entity<ProductPictures>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            //builder.Entity<Router>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Salon>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<SalonBranch>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Service>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Package>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ServiceCategory>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ServicePackage>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ServiceProduct>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            //builder.Entity<Setting>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<User>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            //builder.Entity<UserAuthority>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Warehouse>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<StaffService>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            
            builder.Entity<Staff>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<StaffTitle>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Booking>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<BookingLog>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<BookingDetail>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<BookingStatus>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Commission>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<CommissionDetail>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            //builder.Entity<StaffCommissionGroup>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<CommissionUnit>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            return builder;
        }
    }
}
