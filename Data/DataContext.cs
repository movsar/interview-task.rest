using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataContext : DbContext
    {
        public static string GetCurrentConnectionString()
        {
            var config = new ConfigurationBuilder()
                                  .AddJsonFile("settings.json", optional: false)
                                  .Build();

            return config.GetSection("ConnectionString").Value;
        }
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<TdTask>? TdTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }
    }
}
