using Microsoft.EntityFrameworkCore;
namespace SALON_HAIR_ENTITY.Entities
{
    public static class GlobalQueryFillter
    {
        public static ModelBuilder BuilCustomFillter( ModelBuilder  builder)
        {
             
            builder.Entity<CustomerPackage>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Authority>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<AuthorityRouter>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Booking>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<BookingDetail>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<BookingDetailService>().HasQueryFilter(e => !e.Status.Equals("DELETED"));          
            builder.Entity<BookingLog>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<BookingStatus>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<CommissionPackage>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<CashBook>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<CashBookTransaction>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<CommissionProduct>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<CommissionService>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<CurrencyUnit>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Customer>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<CustomerChannel>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<CustomerSource>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<DiscountUnit>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Invoice>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<InvoiceDetail>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<InvoicePayment>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<InvoiceStaffArrangement>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<InvoiceStatus>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Package>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<PackageSalonBranch>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<PaymentBanking>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<PaymentBankingMethod>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<PaymentMethod>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Photo>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Product>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ProductCategory>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ProductCountUnit>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ProductPictures>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ProductSalonBranch>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ProductSource>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ProductStatus>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ProductUnit>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Router>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Salon>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<SalonBranch>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Service>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ServiceCategory>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ServicePackage>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ServiceProduct>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<ServiceSalonBranch>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Setting>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            //builder.Entity<SettingAdvance>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Staff>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<StaffGroup>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<StaffSalonBranch>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<StaffService>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            //builder.Entity<SysObjectAutoIncreament>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            //builder.Entity<SysScheduleJob>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<User>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<UserAuthority>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<UserSalonBranch>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<Warehouse>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<WarehouseStatus>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<WarehouseTransaction>().HasQueryFilter(e => !e.Status.Equals("DELETED"));
            builder.Entity<WarehouseTransactionDetail>().HasQueryFilter(e => !e.Status.Equals("DELETED"));


            return builder;
        }
    }
}
