using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.StartupService
{
    public static class InsfrastructureService
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(
                Configuration.GetConnectionString("DefaultConnectionString")
                ));
        }

        
    }
}
