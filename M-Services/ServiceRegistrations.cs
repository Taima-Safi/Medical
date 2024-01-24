using M_EF.Data;
using M_Services.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Repos;
using Repos.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static async Task<IApplicationBuilder> SeedData(this WebApplication app)
    }
}
