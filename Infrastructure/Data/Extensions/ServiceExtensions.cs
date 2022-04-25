using System.Text;
using Core.Entities;
using Core.Interfaces.IRepository;
using Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Data.Extensions
{
    public static class ServiceExtensions
    {
        // sqlserver connection service
        public static void ConfigureSqlContext
          (
            this IServiceCollection services,
            IConfiguration configuration
          ) =>
           services.AddDbContext<StoreContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("sqlConnection"))
            );
        //End
        // unitofwork services
        public static void ConfigureUnitOfWork(this IServiceCollection services) =>
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
        //    
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services,IConfiguration configuration){

            var builder = services.AddIdentityCore<User>(q => q.User.RequireUniqueEmail=true);
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole) ,builder.Services);
            builder.AddEntityFrameworkStores<StoreContext>().AddDefaultTokenProviders();
            builder.AddSignInManager<SignInManager<User>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                          ValidIssuer = configuration["Token:Issuer"],
                          ValidateIssuer = true,
                          ValidateAudience=false

                      };
                    }); 
            return services;

        }
    }
}