using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<TdTask> AddAsync(TdTask tdTask)
        {
            _context.TdTasks!.Add(tdTask);
            await _context.SaveChangesAsync();
            return tdTask;
        }

        public async Task<TdTask?> GetByIdAsync(Guid id)
        {
            return await _context.TdTasks!.FirstOrDefaultAsync(p => p.Id == id);
        }

        public IEnumerable<TdTask> ListAll()
        {
            return _context.TdTasks!;
        }

        public IEnumerable<TdTask> ListOverdue()
        {
            return _context.TdTasks!.Where(t => t.DueDate < DateTime.Now);
        }

        public IEnumerable<TdTask> ListPending()
        {
            return _context.TdTasks!.Where(t => t.DueDate > DateTime.Now);
        }

        public async Task UpdateAsync(Guid id, TdTask tdTask)
        {
            var existingTask = await GetByIdAsync(id);
            if (existingTask == null)
            {
                throw new Exception("item not found");
            }

            existingTask.Title = tdTask.Title;
            existingTask.DueDate = tdTask.DueDate;

            _context.SaveChanges();
        }

        public async Task RemoveAsync(Guid id)
        {
            var existingTask = await GetByIdAsync(id);
            if (existingTask == null)
            {
                throw new Exception("item not found");
            }

            _context.TdTasks!.Remove(existingTask);
            await _context.SaveChangesAsync();
        }

        public async Task SetCompletionStatusAsync(Guid id, bool isComplete)
        {
            var existingTask = await GetByIdAsync(id);
            if (existingTask == null)
            {
                throw new Exception("item not found");
            }

            existingTask.SetCompletionStatus(isComplete);
            _context.SaveChanges();
        }
    }
}
