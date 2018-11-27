using Microsoft.EntityFrameworkCore;
using SALON_HAIR_ENTITY.Entities;
namespace SALON_HAIR_ENTITY.Extensions
{
    public static class GlobalQueryFillter
    {
        public static ModelBuilder BuilCustomFillter( ModelBuilder  builder)
        {
         
            builder.Entity<AuthorityRouter>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Booking>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<BookingDetail>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<BookingLog>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<BookingStatus>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Commission>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<CommissionDetail>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<CommissionUnit>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Customer>().HasQueryFilter(e => !e.Status.Equals("DELETED"));    
            builder.Entity<Invoice>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<InvoiceDetail>().HasQueryFilter(e => !e.Status.Equals("DELETED"));          
            builder.Entity<InvoicePayment>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<InvoiceStaffArrangement>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<InvoiceStatus>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Package>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<PaymentBanking>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<PaymentMethod>().HasQueryFilter(e => !e.Status.Equals("DELETED"));       
            builder.Entity<Product>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ProductCategory>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ProductControl>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ProductUnit>().HasQueryFilter(e => !e.Status.Equals("DELETED"));           
            builder.Entity<Salon>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<SalonBranch>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Service>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ServiceCategory>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ServicePackage>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ServiceProduct>().HasQueryFilter(e => !e.Status.Equals("DELETED"));         
            builder.Entity<Staff>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<StaffCommisonGroup>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<StaffService>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<StaffTitle>().HasQueryFilter(e => !e.Status.Equals("DELETED"));          
            builder.Entity<User>().HasQueryFilter(e => !e.Status.Equals("DELETED"));           
            builder.Entity<Warehouse>().HasQueryFilter(e => !e.Status.Equals("DELETED"));

            return builder;
        }
    }
}
