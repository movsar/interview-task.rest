using Data.Models;
using Data.Repository;

namespace Data {
    public class Storage {
        public TaskRepository TdTasks { get; }

        private DataContext _context;
        public Storage(DataContext context, TaskRepository tdTaskRepository) {
            _context = context;
            TdTasks = tdTaskRepository;
            _context.Database.EnsureCreated();
        }

        public async Task<int> SaveChangesAsync() {
            return await _context.SaveChangesAsync();
        }
    }
}