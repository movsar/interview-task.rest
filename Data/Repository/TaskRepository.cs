using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository {
    public class TaskRepository {
        private readonly DataContext _context;

        public TaskRepository(DataContext context) {
            _context = context;
        }

        public async Task Add(TdTask tdTask) {
            _context.TdTasks.Add(tdTask);
            await _context.SaveChangesAsync();
        }
        public async Task<TdTask?> GetById(Guid? id) {
            return await _context.TdTasks.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Update(TdTask tdTask) {
            _context.Attach(tdTask).State = EntityState.Modified;
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!Exists(tdTask.Id)) {
                    throw new Exception($"task with {tdTask.Id} doesn't exist");
                } else {
                    throw;
                }
            }
        }
        public async Task Remove(Guid? id) {
            var tdTask = await GetById(id);
            if (tdTask == null) {
                return;
            }

            _context.Remove(tdTask);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(TdTask tdTask) {
            _context.Remove(tdTask);
            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id) {
            return _context.TdTasks.Any(e => e.Id == id);
        }

        public async Task<List<TdTask>> ToListAsync() {
            return await _context.TdTasks.ToListAsync();
        }

        public IEnumerable<TdTask> GetAll() {
            return _context.TdTasks;
        }

        public IEnumerable<TdTask> ListOverdue() {
            return _context.TdTasks.Where(t => t.DueDate > DateTime.Now);
        }
        public IEnumerable<TdTask> ListPending() {
            return _context.TdTasks.Where(t => t.DueDate < DateTime.Now);
        }
    }
}
