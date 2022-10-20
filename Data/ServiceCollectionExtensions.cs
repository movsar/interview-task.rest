using Data.Interfaces;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(DataContext.GetCurrentConnectionString()));
            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }
}
