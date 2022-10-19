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

        public IEnumerable<TdTask> ListAll() {
            return _context.TdTasks;
        }

        public IEnumerable<TdTask> ListOverdue() {
            return _context.TdTasks.Where(t => t.DueDate < DateTime.Now);
        }

        public IEnumerable<TdTask> ListPending() {
            return _context.TdTasks.Where(t => t.DueDate > DateTime.Now);
        }

        public async Task Update(Guid? id, TdTask tdTask) {
            var existingTask = await GetById(id);
            if (existingTask == null) {
                return;
            }

            existingTask.Title = tdTask.Title;
            existingTask.DueDate = tdTask.DueDate;
            existingTask.IsCompleted = tdTask.IsCompleted;

            _context.SaveChanges();
        }

        public async Task Remove(Guid? id) {
            var taskToRemove = await GetById(id);
            if (taskToRemove == null) {
                return;
            }

            _context.TdTasks.Remove(taskToRemove);
            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid? id) {
            return _context.TdTasks.Any(e => e.Id == id);
        }

    }
}
