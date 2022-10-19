using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models {
    public class DataContext : DbContext {
        public DbSet<TdTask> TdTasks { get; set; }
    }
}
