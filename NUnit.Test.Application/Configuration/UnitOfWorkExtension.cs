using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Test.Application.DataDb;
using NUnit.Test.Application.Domain;
using NUnit.Test.Application.Mapping;
using NUnit.Test.Application.Repository;
using NUnit.Test.Application.Services;

namespace NUnit.Test.Application.Configuration
{

        public static class UnitOfWorkExtension
        {
            public static async Task<IServiceCollection> RegisterDataContext(this IServiceCollection services, string connectionString)
            {
                services.AddDbContext<ApplicationDbContext>(o =>
                {
                    o.UseSqlServer(connectionString);
                });
                services.AddAutoMapper(typeof(AuthMapper));
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IUserRepository, UserRepository>();
               // services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
                services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();
                services.AddScoped<UserManager<User>>();
                services.AddScoped<SignInManager<User>>();
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                    options.AddPolicy("User", policy => policy.RequireRole("User"));
                });
               // await AdminDataSeed.Seed(services.BuildServiceProvider());
                return services;
            }
        }
    }
