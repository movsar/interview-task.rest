using Data.Models;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data {
    public static class ServiceCollectionExtensions {
        public static void RegisterServices(this IServiceCollection services) {
            var cs = "Server=172.17.0.3,1433;User ID=SA;Password=<YourStrong@Passw0rd>;Database=Tasks;Connect Timeout=30;Encrypt=False;";
            services.AddDbContext<DataContext>(options => options.UseSqlServer(cs));
            services.AddScoped<TaskRepository>();
            services.AddScoped<Storage>();
        }
    }
}
