using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SALON_HAIR_CORE.Interface;
using SALON_HAIR_CORE.Service;
using SALON_HAIR_ENTITY.Entities;
using Swashbuckle.AspNetCore.Swagger;
using ULTIL_HELPER;

namespace SALON_HAIR_AUTHENTICATION
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Dbcontext
            services.AddUtilHelperCTNET();
            services.AddDbContextPool<salon_hairContext>( // replace "YourDbContext" with the class name of your DbContext
                options => options.UseMySql(ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection"), // replace with your Connection String
                    mysqlOptions =>
                    {
                        mysqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql); // replace with your Server Version and Type
                    }
            ));
            #endregion
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);          
            services.AddIdentity<IAuthority, IdentityRole>(op => op.ClaimsIdentity.UserIdClaimType = JwtRegisteredClaimNames.Sub);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "EASYSPA ADMIN AUTHENTICATE DOCUMENTS",
                    Description = "Restful webservice API Document for .Netcore",
                    Contact = new Contact
                    {
                        Name = "Văn VT",
                        Email = "vothanhvan711@gmail.com",
                        Url = "vothanhvan.com"
                    }
                });

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
            #region Dependency injection
            services.AddScoped<IAuthority, AuthorityService>();
            services.AddScoped<IAuthorityRouter, AuthorityRouterService>();
            services.AddScoped<IPhoto, PhotoService>();
            services.AddScoped<IProduct, ProductService>();
            services.AddScoped<IProductCategory, ProductCategoryService>();
            services.AddScoped<IProductPictures, ProductPicturesService>();
            services.AddScoped<IRouter, RouterService>();
            services.AddScoped<ISalon, SalonService>();
            services.AddScoped<ISalonBranch, SalonBranchService>();
            services.AddScoped<IService, ServiceService>();
            services.AddScoped<IServiceCategory, ServiceCategoryService>();
            services.AddScoped<IServicePackage, ServicePackageService>();         
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IUserAuthority, UserAuthorityService>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("MyPolicy");
            app.UseForwardedHeaders();
            app.UseSwagger();
            app.UseMvc();
        }
    }
}
