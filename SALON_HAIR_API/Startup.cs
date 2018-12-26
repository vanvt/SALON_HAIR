using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SALON_HAIR_API.BackGround;
using SALON_HAIR_API.Middlewares;
using SALON_HAIR_API.ViewModels;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Service;
using SALON_HAIR_ENTITY.Entities;
using Swashbuckle.AspNetCore.Swagger;

using ULTIL_HELPER;


namespace WebApplication4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SecurityHelper = new SecurityHelper();

        }
        public IConfiguration Configuration { get; }
        public ISecurityHelper SecurityHelper { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<TimedHostedService>();
            services.AddUtilHelperCTNET();
            services.AddMvc(options =>
            {             
                options.Filters.Add(typeof(ActionFilter)); // by type            
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
             .AddJsonOptions(
                options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    // options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //options.SerializerSettings.MaxDepth = 1;
                    //options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;                    
                    ;
                });
            #region Dbcontext
            services.AddDbContextPool<salon_hairContext>( // replace "YourDbContext" with the class name of your DbContext
                
                options => options.UseLazyLoadingProxies(false).UseMySql(ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection"), // replace with your Connection String
                    mysqlOptions =>
                    {
                        mysqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql); // replace with your Server Version and Type
                    }
            ));
            #endregion
            #region AutoMapper
            services.AddAutoMapper();
            #endregion
            #region SwaggerAPI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SALON_HAIR API DOCUMENTS",
                    Description = "Restful webservice API Document for .Netcore",
                    Contact = new Contact
                    {
                        Name = "Văn VT",
                        Email = "vothanhvan711@gmail.com",
                        Url = "vothanhvan.com"
                    }
                });              
                ;

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                // JWT-token authentication by password
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);                
            });
            #endregion
            #region Authentication JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    SecurityHelper.Base64UrlDecode(Configuration["Jwt:Key"])),
                ClockSkew = TimeSpan.Zero,                
                
            };
        });
            #endregion
            #region Enable Cors
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });
            #endregion
            #region Dependency injection
            services.AddScoped<IAuthority, AuthorityService>();
            services.AddScoped<IAuthorityRouter, AuthorityRouterService>();
            services.AddScoped<IBooking, BookingService>();
            services.AddScoped<IBookingDetail, SALON_HAIR_CORE.Service.BookingDetailService>();
            services.AddScoped<IBookingDetailService, BookingDetailServiceService>();

            //services.AddScoped<IBookingDetail, BookingDetailService>();
            services.AddScoped<IBookingLog, BookingLogService>();
            services.AddScoped<IBookingStatus, BookingStatusService>();
            services.AddScoped<ICommissionArrangement, CommissionArrangementService>();
            services.AddScoped<ICommissionPackage, CommissionPackageService>();
            services.AddScoped<ICommissionProduct, CommissionProductService>();
            services.AddScoped<ICommissionService, CommissionServiceService>();
            services.AddScoped<ICustomer, CustomerService>();
            services.AddScoped<ICustomerPackage, CustomerPackageService>();
            services.AddScoped<ICustomerDebtTransactionPayment, CustomerDebtTransactionPaymentService>();
            services.AddScoped<ICustomerDebt, CustomerDebtService>();
            services.AddScoped<ICustomerDebtTransaction, CustomerDebtTransactionService>();
            services.AddScoped<ICashBook, CashBookService>();
            services.AddScoped<ICashBookTransaction, CashBookTransactionService>();
            services.AddScoped<ICashBookTransactionCategory, CashBookTransactionCategoryService>();
            services.AddScoped<ICustomerChannel, CustomerChannelService>();
            services.AddScoped<ICustomerSource, CustomerSourceService>();
            services.AddScoped<ICurrencyUnit, CurrencyUnitService>();
            services.AddScoped<IDiscountUnit, DiscountUnitService>();
            services.AddScoped<IInvoice, InvoiceService>();
            services.AddScoped<IInvoiceDetail, InvoiceDetailService>();
            services.AddScoped<IInvoicePayment, InvoicePaymentService>();
            services.AddScoped<IInvoiceStaffArrangement, InvoiceStaffArrangementService>();
            services.AddScoped<IInvoiceStatus, InvoiceStatusService>();
            services.AddScoped<IPackage, PackageService>();
            services.AddScoped<IPackageSalonBranch,PackageSalonBranchService>();
            services.AddScoped<IPaymentBanking, PaymentBankingService>();
            services.AddScoped<IPaymentMethod, PaymentMethodService>();
            services.AddScoped<IPhoto, PhotoService>();
            services.AddScoped<IProduct, ProductService>();
            services.AddScoped<IProductCategory, ProductCategoryService>();
            services.AddScoped<IProductCountUnit, ProductCountUnitService>();
            services.AddScoped<IProductPictures, ProductPicturesService>();
            services.AddScoped<IProductSalonBranch, ProductSalonBranchService>();
            services.AddScoped<IProductSource, ProductSourceService>();
            services.AddScoped<IProductStatus, ProductStatusService>();
            services.AddScoped<IProductUnit, ProductUnitService>();
            services.AddScoped<IRouter, RouterService>();
            services.AddScoped<ISalon, SalonService>();
            services.AddScoped<ISalonBranch, SalonBranchService>();
            services.AddScoped<IService, ServiceService>();
            services.AddScoped<IServiceCategory, ServiceCategoryService>();
            services.AddScoped<IServicePackage, ServicePackageService>();
            services.AddScoped<IServiceProduct, ServiceProductService>();
            services.AddScoped<IServiceSalonBranch, ServiceSalonBranchService>();
            services.AddScoped<ISetting, SettingService>();
            services.AddScoped<IStaff, SALON_HAIR_CORE.Service.StaffService>();
            services.AddScoped<IStaffGroup, StaffGroupService>();
            services.AddScoped<IStaffSalonBranch, StaffSalonBranchService>();
            services.AddScoped<IStaffService, StaffServiceService>();
            services.AddScoped<ISettingAdvance, SettingAdvanceService>();
            services.AddScoped<ISysObjectAutoIncreament, SysObjectAutoIncreamentService>();
            services.AddScoped<IStatus, StatusService>();
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IUserAuthority, UserAuthorityService>();
            services.AddScoped<IUserSalonBranch, UserSalonBranchService>();
            services.AddScoped<IWarehouse, WarehouseService>();
            services.AddScoped<IWarehouseStatus, WarehouseStatusService>();
            services.AddScoped<IWarehouseTransaction, WarehouseTransactionService>();
            services.AddScoped<IWarehouseTransactionDetail, WarehouseTransactionDetailService>();



            #endregion
            services.AddDirectoryBrowser();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EASYSPA ADMIN API V1");
                c.SwaggerEndpoint("/auth/swagger/v1/swagger.json", "EASYSPA ADMIN AUTHENTICATE API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseMvc();
        }
        public class DomainProfile : Profile
        {
            public DomainProfile()
            {
                CreateMap<Service, ServiceVM>().ForMember(e=>e.ServiceProduct,x=>x.Ignore());
                CreateMap<ServiceVM, Service>().ForMember(e => e.ServiceProduct, x => x.Ignore());
                CreateMap<ServiceProductVM, ServiceProduct>().ForMember(e => e.Product, x => x.Ignore());
                CreateMap<ServiceProduct, ServiceProductVM>().ForMember(e => e.Product, x => x.Ignore());

            }
        }
    }
}
