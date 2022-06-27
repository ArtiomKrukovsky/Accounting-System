using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Сonfectionery.Infrastructure;

namespace Сonfectionery.API.Extensions
{
    public static class ServiceExtensions
    {
        private const string DefaultConnectionString = "DefaultConnection";

        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<СonfectioneryContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString(DefaultConnectionString),
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly("Сonfectionery.Infrastructure");
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });
                }, ServiceLifetime.Scoped  
            );
        }
    }
}