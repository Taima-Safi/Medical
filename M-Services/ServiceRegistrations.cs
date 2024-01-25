using M_Core.UserStatics;
using M_EF.Data;
using M_EF.Entities;
using M_Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Graph.Models;
using Microsoft.IdentityModel.Tokens;
using Repos;
using Repos.IRepos;
using System.Text;

namespace M_Services
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, string? connectionStringConfigName)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionStringConfigName);
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepo, AuthRepo>();

            return services;
        }
        public static IServiceCollection AddApplicationJwtAuth(this IServiceCollection services, JwtConfig configuration)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateActor = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        RequireExpirationTime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration.Issuer,
                        ValidAudience = configuration.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Key)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }


        public static IdentityBuilder AddApplicationIdentity(this IServiceCollection services)
        {
            return services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager<SignInManager<ApplicationUser>>();
        }

        public static async Task<IApplicationBuilder> SeedDataAsync(this WebApplication app)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var AdminRole = new IdentityRole(Roles.Admin);
                var UserRole = new IdentityRole(Roles.User);

                await roleManager.CreateAsync(AdminRole);
                await roleManager.CreateAsync(UserRole);

                var adminUser = new ApplicationUser() { FirstName = "fadmin", LastName = "ladmin", UserName = "a@gmail.com", Email = "a@gmail.com", Gender = GenderType.Male };
                var user = new ApplicationUser() { FirstName = "fuser", LastName = "luser", UserName = "u@gmail.com", Email = "u@gmail.com",Gender = GenderType.Male };

                await userManager.CreateAsync(adminUser, "123");
                await userManager.CreateAsync(user, "123");

                await userManager.AddToRoleAsync(adminUser, Roles.Admin);
                await userManager.AddToRoleAsync(user, Roles.User);

                //await roleManager.AddClaimAsync(AdminRole, GetAdminClaims(Contoller.Post));
            }
            return (IApplicationBuilder)app;
        }
    }
}
