using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SALON_HAIR_ENTITY.Entities
{
    public partial class salon_hairContext : DbContext
    {
        //public salon_hairContext()
        //{
        //}

        public salon_hairContext(DbContextOptions<salon_hairContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authority> Authority { get; set; }
        public virtual DbSet<AuthorityRouter> AuthorityRouter { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<BookingDetail> BookingDetail { get; set; }
        public virtual DbSet<BookingLog> BookingLog { get; set; }
        public virtual DbSet<BookingStatus> BookingStatus { get; set; }
        public virtual DbSet<Commission> Commission { get; set; }
        public virtual DbSet<CommissionDetail> CommissionDetail { get; set; }
        public virtual DbSet<CommissionUnit> CommissionUnit { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerChannel> CustomerChannel { get; set; }
        public virtual DbSet<CustomerSource> CustomerSource { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetail { get; set; }
        public virtual DbSet<InvoicePayment> InvoicePayment { get; set; }
        public virtual DbSet<InvoiceStaffArrangement> InvoiceStaffArrangement { get; set; }
        public virtual DbSet<InvoiceStatus> InvoiceStatus { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<PaymentBanking> PaymentBanking { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductControl> ProductControl { get; set; }
        public virtual DbSet<ProductPictures> ProductPictures { get; set; }
        public virtual DbSet<ProductSalonBranch> ProductSalonBranch { get; set; }
        public virtual DbSet<ProductSource> ProductSource { get; set; }
        public virtual DbSet<ProductStatus> ProductStatus { get; set; }
        public virtual DbSet<ProductUnit> ProductUnit { get; set; }
        public virtual DbSet<Router> Router { get; set; }
        public virtual DbSet<Salon> Salon { get; set; }
        public virtual DbSet<SalonBranch> SalonBranch { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceCategory> ServiceCategory { get; set; }
        public virtual DbSet<ServicePackage> ServicePackage { get; set; }
        public virtual DbSet<ServiceProduct> ServiceProduct { get; set; }
        public virtual DbSet<ServiceSalonBranch> ServiceSalonBranch { get; set; }
        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffCommisonGroup> StaffCommisonGroup { get; set; }
        public virtual DbSet<StaffSalonBranch> StaffSalonBranch { get; set; }
        public virtual DbSet<StaffService> StaffService { get; set; }
        public virtual DbSet<StaffTitle> StaffTitle { get; set; }
        public virtual DbSet<SysObjectAutoIncreament> SysObjectAutoIncreament { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserAuthority> UserAuthority { get; set; }
        public virtual DbSet<UserSalonBranch> UserSalonBranch { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }

        // Unable to generate entity type for table 'Areas'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=dev-db-sv.easyspa.vn;Database=salon_hair;uid=easyspavn;pwd=Easyspavn@2017;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authority>(entity =>
            {
                entity.ToTable("authority");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(45)")
                    .HasDefaultValueSql("'ENABLE'");
            });

            modelBuilder.Entity<AuthorityRouter>(entity =>
            {
                entity.HasKey(e => new { e.AuthorityId, e.RouterId });

                entity.ToTable("authority_router");

                entity.HasIndex(e => e.RouterId)
                    .HasName("authority_router_router_idx");

                entity.Property(e => e.AuthorityId)
                    .HasColumnName("authority_Id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RouterId)
                    .HasColumnName("router_Id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Active).HasColumnType("bit(1)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Authority)
                    .WithMany(p => p.AuthorityRouter)
                    .HasForeignKey(d => d.AuthorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authority_router_authority");

                entity.HasOne(d => d.Router)
                    .WithMany(p => p.AuthorityRouter)
                    .HasForeignKey(d => d.RouterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authority_router_router");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("booking");

                entity.HasIndex(e => e.BookingStatusId)
                    .HasName("booking_status_idx");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("booking_customer_idx");

                entity.HasIndex(e => e.SalonBranchId)
                    .HasName("booking_salon_branch_idx");

                entity.HasIndex(e => e.SalonId)
                    .HasName("booking_salon_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BookingStatusId)
                    .HasColumnName("booking_status_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("text");

                entity.Property(e => e.NumberCustomer)
                    .HasColumnName("number_customer")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SalonBranchId)
                    .HasColumnName("salon_branch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.BookingStatus)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.BookingStatusId)
                    .HasConstraintName("booking_status");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("booking_customer");

                entity.HasOne(d => d.SalonBranch)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.SalonBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booking_salon_branch");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booking_salon");
            });

            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.ToTable("booking_detail");

                entity.HasIndex(e => e.BookingId)
                    .HasName("booking_idx");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("service_id_idx");

                entity.HasIndex(e => e.StaffId)
                    .HasName("booking_detail_staff_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BookingId)
                    .HasColumnName("booking_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.StaffId)
                    .HasColumnName("staff_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingDetail)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booking_detail_booking");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.BookingDetail)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booking_detail_service");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.BookingDetail)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("booking_detail_staff");
            });

            modelBuilder.Entity<BookingLog>(entity =>
            {
                entity.ToTable("booking_log");

                entity.HasIndex(e => e.BookingId)
                    .HasName("booking_log_booking_idx");

                entity.HasIndex(e => e.BookingStatusId)
                    .HasName("booking_log_status_idx");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("booking_log_customer_idx");

                entity.HasIndex(e => e.SalonBranchId)
                    .HasName("booking_log_salon_branch_idx");

                entity.HasIndex(e => e.SalonId)
                    .HasName("booking_log_salon_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BookingId)
                    .HasColumnName("booking_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BookingStatusId)
                    .HasColumnName("booking_status_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("text");

                entity.Property(e => e.NumberCustomer)
                    .HasColumnName("number_customer")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SalonBranchId)
                    .HasColumnName("salon_branch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingLog)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("booking_log_booking");

                entity.HasOne(d => d.BookingStatus)
                    .WithMany(p => p.BookingLog)
                    .HasForeignKey(d => d.BookingStatusId)
                    .HasConstraintName("booking_log_status");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BookingLog)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("booking_log_customer");

                entity.HasOne(d => d.SalonBranch)
                    .WithMany(p => p.BookingLog)
                    .HasForeignKey(d => d.SalonBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booking_log_salon_branch");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.BookingLog)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booking_log_salon");
            });

            modelBuilder.Entity<BookingStatus>(entity =>
            {
                entity.ToTable("booking_status");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Commission>(entity =>
            {
                entity.ToTable("commission");

                entity.HasIndex(e => e.LimitCommisionUnitId)
                    .HasName("commission_limit_unit_idx");

                entity.HasIndex(e => e.RetailCommisionUnitId)
                    .HasName("commission_retail_unit_idx");

                entity.HasIndex(e => e.SalonBranchId)
                    .HasName("commission_salon_bracnh_idx");

                entity.HasIndex(e => e.SalonId)
                    .HasName("commission_salon_idx");

                entity.HasIndex(e => e.ServiceCategoryId)
                    .HasName("commission_service_categoru_idx");

                entity.HasIndex(e => e.StaffCommisonGroupId)
                    .HasName("commission_staff_category");

                entity.HasIndex(e => e.WholesaleCommisionUnitId)
                    .HasName("commission_whosale_unit_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.LimitCommisionUnitId)
                    .HasColumnName("limit_commision_unit_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.LimitCommisionValue).HasColumnName("limit_commision_value");

                entity.Property(e => e.RetailCommisionUnitId)
                    .HasColumnName("retail_commision_unit_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RetailCommisionValue).HasColumnName("retail_commision_value");

                entity.Property(e => e.SalonBranchId)
                    .HasColumnName("salon_branch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ServiceCategoryId)
                    .HasColumnName("service_category_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.StaffCommisonGroupId)
                    .HasColumnName("staff_commison_group_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.WholesaleCommisionUnitId)
                    .HasColumnName("wholesale_commision_unit_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.WholesaleCommisionValue).HasColumnName("wholesale_commision_value");

                entity.HasOne(d => d.LimitCommisionUnit)
                    .WithMany(p => p.CommissionLimitCommisionUnit)
                    .HasForeignKey(d => d.LimitCommisionUnitId)
                    .HasConstraintName("commission_limit_unit");

                entity.HasOne(d => d.RetailCommisionUnit)
                    .WithMany(p => p.CommissionRetailCommisionUnit)
                    .HasForeignKey(d => d.RetailCommisionUnitId)
                    .HasConstraintName("commission_retail_unit");

                entity.HasOne(d => d.SalonBranch)
                    .WithMany(p => p.Commission)
                    .HasForeignKey(d => d.SalonBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commission_salon_bracnh");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.Commission)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commission_salon");

                entity.HasOne(d => d.ServiceCategory)
                    .WithMany(p => p.Commission)
                    .HasForeignKey(d => d.ServiceCategoryId)
                    .HasConstraintName("commission_service_categoru");

                entity.HasOne(d => d.StaffCommisonGroup)
                    .WithMany(p => p.Commission)
                    .HasForeignKey(d => d.StaffCommisonGroupId)
                    .HasConstraintName("commission_staff_category");

                entity.HasOne(d => d.WholesaleCommisionUnit)
                    .WithMany(p => p.CommissionWholesaleCommisionUnit)
                    .HasForeignKey(d => d.WholesaleCommisionUnitId)
                    .HasConstraintName("commission_whosale_unit");
            });

            modelBuilder.Entity<CommissionDetail>(entity =>
            {
                entity.ToTable("commission_detail");

                entity.HasIndex(e => e.CommissionId)
                    .HasName("commission_detail_commision_idx");

                entity.HasIndex(e => e.LimitCommisionUnitId)
                    .HasName("commission_detail_limit_unit_idx");

                entity.HasIndex(e => e.RetailCommisionUnitId)
                    .HasName("commission_detail_retail_unit_idx");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("commission_detail_service_idx");

                entity.HasIndex(e => e.WholesaleCommisionUnitId)
                    .HasName("commission_detail_whosale_unit_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CommissionId)
                    .HasColumnName("commission_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.LimitCommisionUnitId)
                    .HasColumnName("limit_commision_unit_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.LimitCommisionValue).HasColumnName("limit_commision_value");

                entity.Property(e => e.RetailCommisionUnitId)
                    .HasColumnName("retail_commision_unit_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RetailCommisionValue).HasColumnName("retail_commision_value");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.WholesaleCommisionUnitId)
                    .HasColumnName("wholesale_commision_unit_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.WholesaleCommisionValue).HasColumnName("wholesale_commision_value");

                entity.HasOne(d => d.Commission)
                    .WithMany(p => p.CommissionDetail)
                    .HasForeignKey(d => d.CommissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commission_detail_commision");

                entity.HasOne(d => d.LimitCommisionUnit)
                    .WithMany(p => p.CommissionDetailLimitCommisionUnit)
                    .HasForeignKey(d => d.LimitCommisionUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commission_detail_limit_unit");

                entity.HasOne(d => d.RetailCommisionUnit)
                    .WithMany(p => p.CommissionDetailRetailCommisionUnit)
                    .HasForeignKey(d => d.RetailCommisionUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commission_detail_retail_unit");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.CommissionDetail)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("commission_detail_service");

                entity.HasOne(d => d.WholesaleCommisionUnit)
                    .WithMany(p => p.CommissionDetailWholesaleCommisionUnit)
                    .HasForeignKey(d => d.WholesaleCommisionUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commission_detail_whosale_unit");
            });

            modelBuilder.Entity<CommissionUnit>(entity =>
            {
                entity.ToTable("commission_unit");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.ChannelCustomerId)
                    .HasName("customer_channel_idx");

                entity.HasIndex(e => e.PhotoId)
                    .HasName("customer_photo_idx");

                entity.HasIndex(e => e.SalonBranchId)
                    .HasName("customer_salon_branch_idx");

                entity.HasIndex(e => e.SalonId)
                    .HasName("customer_salon_idx");

                entity.HasIndex(e => e.SoucreCustomerId)
                    .HasName("customer_source_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ChannelCustomerId)
                    .HasColumnName("channel_customer_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("text");

                entity.Property(e => e.PhotoId)
                    .HasColumnName("photo_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonBranchId)
                    .HasColumnName("salon_branch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SoucreCustomerId)
                    .HasColumnName("soucre_customer_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.ChannelCustomer)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.ChannelCustomerId)
                    .HasConstraintName("customer_channel");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("customer_photo");

                entity.HasOne(d => d.SalonBranch)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.SalonBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customer_salon_branch");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customer_salon");

                entity.HasOne(d => d.SoucreCustomer)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.SoucreCustomerId)
                    .HasConstraintName("customer_source");
            });

            modelBuilder.Entity<CustomerChannel>(entity =>
            {
                entity.ToTable("customer_channel");

                entity.HasIndex(e => e.SalonId)
                    .HasName("customer_channel_salon_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IsDefault)
                    .HasColumnName("is_default")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("text");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.CustomerChannel)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customer_channel_salon");
            });

            modelBuilder.Entity<CustomerSource>(entity =>
            {
                entity.ToTable("customer_source");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IsDefault)
                    .HasColumnName("is_default")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("text");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("invoice");

                entity.HasIndex(e => e.CashierId)
                    .HasName("invoice_cashier_idx");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("invoice_customer_idx");

                entity.HasIndex(e => e.DiscountUnitId)
                    .HasName("discount_commision_unit_idx");

                entity.HasIndex(e => e.InvoiceStatusId)
                    .HasName("invoice_status_idx");

                entity.HasIndex(e => e.SalesmanId)
                    .HasName("invoice_salesman_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CashierId)
                    .HasColumnName("cashier_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DiscountUnitId)
                    .HasColumnName("discount_unit_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.DiscountUnitValue).HasColumnName("discount_unit_value");

                entity.Property(e => e.ExcessCash)
                    .HasColumnName("excess_cash")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.InvoiceStatusId)
                    .HasColumnName("invoice_status_id")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsDisplay)
                    .IsRequired()
                    .HasColumnName("is_display")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("text");

                entity.Property(e => e.NoteArrangement)
                    .HasColumnName("note_arrangement")
                    .HasColumnType("text");

                entity.Property(e => e.NotePayment)
                    .HasColumnName("note_payment")
                    .HasColumnType("text");

                entity.Property(e => e.SalesmanId)
                    .HasColumnName("salesman_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonBranchId)
                    .HasColumnName("salon_branch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(20,3)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Cashier)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.CashierId)
                    .HasConstraintName("invoice_cashier");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("invoice_customer");

                entity.HasOne(d => d.DiscountUnit)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.DiscountUnitId)
                    .HasConstraintName("discount_commision_unit");

                entity.HasOne(d => d.InvoiceStatus)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.InvoiceStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("invoice_status_status");

                entity.HasOne(d => d.Salesman)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.SalesmanId)
                    .HasConstraintName("invoice_salesman");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.ToTable("invoice_detail");

                entity.HasIndex(e => e.DiscountUnitId)
                    .HasName("invoice_detail_commission_unit_idx");

                entity.HasIndex(e => e.InvoiceId)
                    .HasName("invoice_detail_invoice_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DiscountUnitId)
                    .HasColumnName("discount_unit_id")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.DiscountValue)
                    .HasColumnName("discount_value")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.InvoiceId)
                    .HasColumnName("invoice_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ObjectCode)
                    .HasColumnName("object_code")
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.ObjectId)
                    .HasColumnName("object_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ObjectName)
                    .HasColumnName("object_name")
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.ObjectPrice)
                    .HasColumnName("object_price")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasColumnName("object_type")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceDetail)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("invoice_detail_invoice");
            });

            modelBuilder.Entity<InvoicePayment>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceId, e.InvoiceMethodId });

                entity.ToTable("invoice_payment");

                entity.HasIndex(e => e.InvoiceBankingId)
                    .HasName("invoice_payment_detail_banking_idx");

                entity.HasIndex(e => e.InvoiceId)
                    .HasName("invoice_payment_detail_invoice_id_idx");

                entity.HasIndex(e => e.InvoiceMethodId)
                    .HasName("invoice_payment_detail_payment_method_idx");

                entity.Property(e => e.InvoiceId)
                    .HasColumnName("invoice_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.InvoiceMethodId)
                    .HasColumnName("invoice_method_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.InvoiceBankingId)
                    .HasColumnName("invoice_banking_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.InvoiceBanking)
                    .WithMany(p => p.InvoicePayment)
                    .HasForeignKey(d => d.InvoiceBankingId)
                    .HasConstraintName("invoice_payment_detail_banking");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoicePayment)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("invoice_payment_detail_invoice_id");

                entity.HasOne(d => d.InvoiceMethod)
                    .WithMany(p => p.InvoicePayment)
                    .HasForeignKey(d => d.InvoiceMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("invoice_payment_detail_payment_method");
            });

            modelBuilder.Entity<InvoiceStaffArrangement>(entity =>
            {
                entity.ToTable("invoice_staff_arrangement");

                entity.HasIndex(e => e.InvoiceDetailId)
                    .HasName("arrangement_invoice_detail_idx");

                entity.HasIndex(e => e.InvoiceId)
                    .HasName("arrangement_invoice_idx");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("arrangement_service_idx");

                entity.HasIndex(e => e.StaffId)
                    .HasName("arrangement_staff_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.InvoiceDetailId)
                    .HasColumnName("invoice_detail_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.InvoiceId)
                    .HasColumnName("invoice_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.StaffId)
                    .HasColumnName("staff_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.InvoiceDetail)
                    .WithMany(p => p.InvoiceStaffArrangement)
                    .HasForeignKey(d => d.InvoiceDetailId)
                    .HasConstraintName("arrangement_invoice_detail");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceStaffArrangement)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("arrangement_invoice");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.InvoiceStaffArrangement)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("arrangement_service");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.InvoiceStaffArrangement)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("arrangement_staff");
            });

            modelBuilder.Entity<InvoiceStatus>(entity =>
            {
                entity.ToTable("invoice_status");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.ToTable("package");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Label)
                    .HasColumnName("label")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OriginalPrice)
                    .HasColumnName("original_price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.PriceText)
                    .HasColumnName("price_text")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SalonBranchId)
                    .HasColumnName("salon_branch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<PaymentBanking>(entity =>
            {
                entity.ToTable("payment_banking");

                entity.HasIndex(e => e.PaymentMethodId)
                    .HasName("payment_banking_method_idx");

                entity.HasIndex(e => e.SalonId)
                    .HasName("payment_banking_salon_idx_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BankHolder)
                    .HasColumnName("bank_holder")
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.BankName)
                    .HasColumnName("bank_name")
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.BankNumber)
                    .HasColumnName("bank_number")
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PaymentMethodId)
                    .HasColumnName("payment_method_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.PaymentBanking)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("payment_banking_method");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.PaymentBanking)
                    .HasForeignKey(d => d.SalonId)
                    .HasConstraintName("payment_banking_salon_idx");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("payment_method");

                entity.HasIndex(e => e.SalonId)
                    .HasName("payment_method_salon_idx_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.PaymentMethod)
                    .HasForeignKey(d => d.SalonId)
                    .HasConstraintName("payment_method_salon_idx");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("photo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.DataContentType)
                    .HasColumnName("data_content_type")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OriginalName)
                    .HasColumnName("original_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(45)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Width).HasColumnName("width");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.PhotoId)
                    .HasName("photo_product_idx");

                entity.HasIndex(e => e.ProductCategoryId)
                    .HasName("FKcwclrqu392y86y0pmyrsi649r");

                entity.HasIndex(e => e.ProductStatusId)
                    .HasName("product_status_idx1");

                entity.HasIndex(e => e.SalonBranchCreateId)
                    .HasName("product_salon_branch_idx");

                entity.HasIndex(e => e.SalonId)
                    .HasName("product_salon_idx");

                entity.HasIndex(e => e.SourceId)
                    .HasName("product_source_idx");

                entity.HasIndex(e => e.Status)
                    .HasName("product_status_idx");

                entity.HasIndex(e => e.UnitId)
                    .HasName("product_unit_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.IsLimit)
                    .HasColumnName("is_limit")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Label)
                    .HasColumnName("label")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PhotoId)
                    .HasColumnName("photo_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.PriceText)
                    .HasColumnName("price_text")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ProductCategoryId)
                    .HasColumnName("product_category_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ProductStatusId)
                    .HasColumnName("product_status_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonBranchCreateId)
                    .HasColumnName("salon_branch_create_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SourceId)
                    .HasColumnName("source_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.UnitId)
                    .HasColumnName("unit_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Volume).HasColumnName("volume");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("photo_product");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .HasConstraintName("FKcwclrqu392y86y0pmyrsi649r");

                entity.HasOne(d => d.ProductStatus)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductStatusId)
                    .HasConstraintName("product_status");

                entity.HasOne(d => d.SalonBranchCreate)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SalonBranchCreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_salon_branch");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_salon");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SourceId)
                    .HasConstraintName("product_source");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("product_unit");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("product_category");

                entity.HasIndex(e => e.SalonId)
                    .HasName("product_category_salon_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.ProductCategory)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_category_salon");
            });

            modelBuilder.Entity<ProductControl>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.SalonBranchId });

                entity.ToTable("product_control");

                entity.HasIndex(e => e.SalonBranchId)
                    .HasName("product_control_salon_branch_idx");

                entity.HasIndex(e => e.SalonId)
                    .HasName("product_control_salon_idx");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonBranchId)
                    .HasColumnName("salon_branch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductControl)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_control_product");

                entity.HasOne(d => d.SalonBranch)
                    .WithMany(p => p.ProductControl)
                    .HasForeignKey(d => d.SalonBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_control_salon_branch");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.ProductControl)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_control_salon");
            });

            modelBuilder.Entity<ProductPictures>(entity =>
            {
                entity.HasKey(e => new { e.ProductsId, e.PicturesId });

                entity.ToTable("product_pictures");

                entity.HasIndex(e => e.PicturesId)
                    .HasName("FKidirrixwqfs9ubbikoos9gvro");

                entity.Property(e => e.ProductsId)
                    .HasColumnName("products_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.PicturesId)
                    .HasColumnName("pictures_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(45)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.HasOne(d => d.Pictures)
                    .WithMany(p => p.ProductPictures)
                    .HasForeignKey(d => d.PicturesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKidirrixwqfs9ubbikoos9gvro");

                entity.HasOne(d => d.Products)
                    .WithMany(p => p.ProductPictures)
                    .HasForeignKey(d => d.ProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKtf058faxpuj2et8hvsy4u0ctb");
            });

            modelBuilder.Entity<ProductSalonBranch>(entity =>
            {
                entity.HasKey(e => new { e.SalonId, e.ProductId });

                entity.ToTable("product_salon_branch");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<ProductSource>(entity =>
            {
                entity.ToTable("product_source");

                entity.HasIndex(e => e.SalonId)
                    .HasName("product_source_salon_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.ProductSource)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_source_salon");
            });

            modelBuilder.Entity<ProductStatus>(entity =>
            {
                entity.ToTable("product_status");

                entity.HasIndex(e => e.SalonId)
                    .HasName("product_status_salon_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.ProductStatus)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_status_salon");
            });

            modelBuilder.Entity<ProductUnit>(entity =>
            {
                entity.ToTable("product_unit");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Router>(entity =>
            {
                entity.ToTable("router");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Action)
                    .HasColumnName("action")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Controller)
                    .HasColumnName("controller")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasColumnType("varchar(400)");

                entity.Property(e => e.IsSystemFunction)
                    .HasColumnName("is_system_function")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.OrderDisplay)
                    .HasColumnName("order_display")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasColumnType("varchar(400)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(45)")
                    .HasDefaultValueSql("'ENABLE'");
            });

            modelBuilder.Entity<Salon>(entity =>
            {
                entity.ToTable("salon");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Cover).HasColumnName("cover");

                entity.Property(e => e.CoverContentType)
                    .HasColumnName("cover_content_type")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasColumnName("currency")
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Logo).HasColumnName("logo");

                entity.Property(e => e.LogoContentType)
                    .HasColumnName("logo_content_type")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OpenHour)
                    .HasColumnName("open_hour")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SpaStatus)
                    .HasColumnName("spa_status")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.WebSite)
                    .HasColumnName("web_site")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<SalonBranch>(entity =>
            {
                entity.ToTable("salon_branch");

                entity.HasIndex(e => e.SalonId)
                    .HasName("FKe2cm16d9d1m7evlk5fb2qnnvi");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ActiveStep)
                    .HasColumnName("active_step")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OpenHour)
                    .HasColumnName("open_hour")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OpenHourFrom)
                    .HasColumnName("open_hour_from")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OpenHourFromValue)
                    .HasColumnName("open_hour_from_value")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OpenHourMinuteFromValue)
                    .HasColumnName("open_hour_minute_from_value")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OpenHourMinuteToValue)
                    .HasColumnName("open_hour_minute_to_value")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OpenHourTo)
                    .HasColumnName("open_hour_to")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OpenHourToValue)
                    .HasColumnName("open_hour_to_value")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SettingStep)
                    .HasColumnName("setting_step")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.SalonBranch)
                    .HasForeignKey(d => d.SalonId)
                    .HasConstraintName("FKe2cm16d9d1m7evlk5fb2qnnvi");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("service");

                entity.HasIndex(e => e.SalonBranchCreateId)
                    .HasName("service_salon_branch_idx");

                entity.HasIndex(e => e.SalonId)
                    .HasName("service_salon_idx");

                entity.HasIndex(e => e.ServiceCategoryId)
                    .HasName("FKowiok9o2e4m07fvmifjnvwtd2");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.SalonBranchCreateId)
                    .HasColumnName("salon_branch_create_ id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ServiceCategoryId)
                    .HasColumnName("service_category_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TimeValue)
                    .HasColumnName("time_value")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.SalonBranchCreate)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.SalonBranchCreateId)
                    .HasConstraintName("service_salon_branch");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service_salon");

                entity.HasOne(d => d.ServiceCategory)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.ServiceCategoryId)
                    .HasConstraintName("FKowiok9o2e4m07fvmifjnvwtd2");
            });

            modelBuilder.Entity<ServiceCategory>(entity =>
            {
                entity.ToTable("service_category");

                entity.HasIndex(e => e.SalonId)
                    .HasName("service_category_salon_idx");

                entity.HasIndex(e => e.Status)
                    .HasName("service_category_status_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Slug)
                    .HasColumnName("slug")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.ServiceCategory)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service_category_salon");
            });

            modelBuilder.Entity<ServicePackage>(entity =>
            {
                entity.HasKey(e => new { e.ServiceId, e.PackageId });

                entity.ToTable("service_package");

                entity.HasIndex(e => e.PackageId)
                    .HasName("paack_service_package_idx");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("packge_service_idx");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.PackageId)
                    .HasColumnName("package_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.ServicePackage)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("paack_service_package");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServicePackage)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("packge_service");
            });

            modelBuilder.Entity<ServiceProduct>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ServiceId });

                entity.ToTable("service_product");

                entity.HasIndex(e => e.ProductId)
                    .HasName("service_product_product_idx");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("service_product_service_idx");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Quota)
                    .HasColumnName("quota")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ServiceProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service_product_product");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceProduct)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("service_product_service");
            });

            modelBuilder.Entity<ServiceSalonBranch>(entity =>
            {
                entity.HasKey(e => new { e.SalonBranchId, e.ServiceId });

                entity.ToTable("service_salon_branch");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("service_salon_branch_service_idx");

                entity.Property(e => e.SalonBranchId)
                    .HasColumnName("salon_branch _id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.SalonBranch)
                    .WithMany(p => p.ServiceSalonBranch)
                    .HasForeignKey(d => d.SalonBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service_salon_branch_branch");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceSalonBranch)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service_salon_branch_service");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("setting");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Field)
                    .IsRequired()
                    .HasColumnName("field")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.FieldDb)
                    .HasColumnName("field_db")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.FieldDisplay)
                    .HasColumnName("field_display")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.FieldType)
                    .HasColumnName("field_type")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IsDisplay)
                    .IsRequired()
                    .HasColumnName("is_display")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.Oder)
                    .HasColumnName("oder")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Searchalbe)
                    .HasColumnName("searchalbe")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(45)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Table)
                    .HasColumnName("table")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.TableDb)
                    .HasColumnName("table_db")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.TableDisplay)
                    .HasColumnName("table_display")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.TableId)
                    .HasColumnName("table_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TableType)
                    .HasColumnName("table_type")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");

                entity.HasIndex(e => e.PhotoId)
                    .HasName("staff_photo_idx");

                entity.HasIndex(e => e.SalonId)
                    .HasName("staff_salon_idx");

                entity.HasIndex(e => e.StaffCommisionGroupId)
                    .HasName("staff_category_idx");

                entity.HasIndex(e => e.StaffTitleId)
                    .HasName("staff_title_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.IsCasual)
                    .IsRequired()
                    .HasColumnName("is_casual")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.IsWorkAllService)
                    .IsRequired()
                    .HasColumnName("is_work_all_service")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PhotoId)
                    .HasColumnName("photo_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.StaffCommisionGroupId)
                    .HasColumnName("staff_commision_group_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.StaffTitleId)
                    .HasColumnName("staff_title_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("staff_photo");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_salon");

                entity.HasOne(d => d.StaffCommisionGroup)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.StaffCommisionGroupId)
                    .HasConstraintName("staff_category");

                entity.HasOne(d => d.StaffTitle)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.StaffTitleId)
                    .HasConstraintName("staff_title");
            });

            modelBuilder.Entity<StaffCommisonGroup>(entity =>
            {
                entity.ToTable("staff_commison_group");

                entity.HasIndex(e => e.PhotoId)
                    .HasName("staff_category_photo_idx");

                entity.HasIndex(e => e.SalonBranchId)
                    .HasName("staff_category_salon_brach_idx");

                entity.HasIndex(e => e.SalonId)
                    .HasName("staff_category_salon_idx");

                entity.HasIndex(e => e.StaffTitleId)
                    .HasName("staff_category_title_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PhotoId)
                    .HasColumnName("photo_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonBranchId)
                    .HasColumnName("salon_branch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.StaffTitleId)
                    .HasColumnName("staff_title_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.StaffCommisonGroup)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("staff_category_photo");

                entity.HasOne(d => d.SalonBranch)
                    .WithMany(p => p.StaffCommisonGroup)
                    .HasForeignKey(d => d.SalonBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_category_salon_brach");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.StaffCommisonGroup)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_category_salon");

                entity.HasOne(d => d.StaffTitle)
                    .WithMany(p => p.StaffCommisonGroup)
                    .HasForeignKey(d => d.StaffTitleId)
                    .HasConstraintName("staff_category_title");
            });

            modelBuilder.Entity<StaffSalonBranch>(entity =>
            {
                entity.HasKey(e => new { e.StaffId, e.SalonBranchId });

                entity.ToTable("staff_salon_branch");

                entity.HasIndex(e => e.SalonBranchId)
                    .HasName("staff_salon_branch_branch_idx");

                entity.HasIndex(e => e.StaffId)
                    .HasName("staff_salon_branch_staff_idx");

                entity.Property(e => e.StaffId)
                    .HasColumnName("staff_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonBranchId)
                    .HasColumnName("salon_branch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.SalonBranch)
                    .WithMany(p => p.StaffSalonBranch)
                    .HasForeignKey(d => d.SalonBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_salon_branch_branch");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffSalonBranch)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_salon_branch_staff");
            });

            modelBuilder.Entity<StaffService>(entity =>
            {
                entity.HasKey(e => new { e.ServiceId, e.StaffId });

                entity.ToTable("staff_service");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("staff_major_service_idx");

                entity.HasIndex(e => e.StaffId)
                    .HasName("staff_major_staff_idx");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.StaffId)
                    .HasColumnName("staff_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.StaffService)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_major_service");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffService)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_major_staff");
            });

            modelBuilder.Entity<StaffTitle>(entity =>
            {
                entity.ToTable("staff_title");

                entity.HasIndex(e => e.SalonBranchId)
                    .HasName("staff_title_salon_brach_idx");

                entity.HasIndex(e => e.SalonId)
                    .HasName("staff_title_salon_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SalonBranchId)
                    .HasColumnName("salon_branch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.SalonBranch)
                    .WithMany(p => p.StaffTitle)
                    .HasForeignKey(d => d.SalonBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_title_salon_brach");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.StaffTitle)
                    .HasForeignKey(d => d.SalonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_title_salon");
            });

            modelBuilder.Entity<SysObjectAutoIncreament>(entity =>
            {
                entity.HasKey(e => new { e.ObjectName, e.SpaId });

                entity.ToTable("sys_object_auto_increament");

                entity.Property(e => e.ObjectName)
                    .HasColumnName("object_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.SpaId)
                    .HasColumnName("spa_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ObjectIndex)
                    .HasColumnName("object_index")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email)
                    .HasName("UK_b2snn5my1hi0lxvujtu3a4t35")
                    .IsUnique();

                entity.HasIndex(e => e.Login)
                    .HasName("UK_p1u3ui9mrv3ia68thhyruocw3")
                    .IsUnique();

                entity.HasIndex(e => e.PhotoId)
                    .HasName("user_photo_idx");

                entity.HasIndex(e => e.SalonBranchCurrentId)
                    .HasName("user_salon_branch_default_idx");

                entity.HasIndex(e => e.SalonBranchId)
                    .HasName("user_salon_branch_idx");

                entity.HasIndex(e => e.SalonId)
                    .HasName("user_salon_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Activated)
                    .HasColumnName("activated")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ActivationKey)
                    .HasColumnName("activation_key")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("image_url")
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.LangKey)
                    .HasColumnName("lang_key")
                    .HasColumnType("varchar(5)");

                entity.Property(e => e.LastModifiedBy)
                    .HasColumnName("last_modified_by")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnName("last_modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .HasColumnType("varchar(60)");

                entity.Property(e => e.PhotoId)
                    .HasColumnName("photo_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ResetDate)
                    .HasColumnName("reset_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ResetKey)
                    .HasColumnName("reset_key")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.SalonBranchCurrentId)
                    .HasColumnName("salon_branch_current_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonBranchId)
                    .HasColumnName("salon_branch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("user_photo");

                entity.HasOne(d => d.SalonBranchCurrent)
                    .WithMany(p => p.UserSalonBranchCurrent)
                    .HasForeignKey(d => d.SalonBranchCurrentId)
                    .HasConstraintName("user_salon_branch_default");

                entity.HasOne(d => d.SalonBranch)
                    .WithMany(p => p.UserSalonBranch)
                    .HasForeignKey(d => d.SalonBranchId)
                    .HasConstraintName("user_salon_branch");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.SalonId)
                    .HasConstraintName("user_salon");
            });

            modelBuilder.Entity<UserAuthority>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.AuthorityId });

                entity.ToTable("user_authority");

                entity.HasIndex(e => e.AuthorityId)
                    .HasName("sdf123123123dsfdsfds_idx");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AuthorityId)
                    .HasColumnName("authority_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(45)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Authority)
                    .WithMany(p => p.UserAuthority)
                    .HasForeignKey(d => d.AuthorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sdf123123123dsfdsfds");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAuthority)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK6e78wvg39g39p5vi0a9t5f3");
            });

            modelBuilder.Entity<UserSalonBranch>(entity =>
            {
                entity.HasKey(e => new { e.SpaBranchId, e.UserId });

                entity.ToTable("user_salon_branch");

                entity.Property(e => e.SpaBranchId)
                    .HasColumnName("spa_branch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(45)")
                    .HasDefaultValueSql("'ENABLE'");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("warehouse");

                entity.HasIndex(e => e.ProductId)
                    .HasName("warehouse_product_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Lable)
                    .HasColumnName("lable")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SalonBranchId)
                    .HasColumnName("salon_branch_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SalonId)
                    .HasColumnName("salon_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Warehouse)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("warehouse_product");
            });
        }
    }
}
