using Data.Models;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data {
    public static class ServiceCollectionExtensions {
        public static void RegisterServices(this IServiceCollection services) {
            var connectionString = "Server=localhost, 1433;Database=Tasks;Trusted_Connection=True;";

            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString ?? throw new InvalidOperationException("Connection string 'WarehouseContext' not found.")));
            services.AddScoped<TaskRepository>();
            services.AddScoped<Storage>();
        }
    }
}
