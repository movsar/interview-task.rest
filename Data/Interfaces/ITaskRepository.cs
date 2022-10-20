using Data.Models;

namespace Data.Interfaces
{
    public interface ITaskRepository
    {
        Task<TdTask> AddAsync(TdTask tdTask);
        Task<TdTask?> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task<int> SaveChangesAsync();
        Task SetCompletionStatusAsync(Guid id, bool isComplete);
        Task UpdateAsync(Guid id, TdTask tdTask);
        IEnumerable<TdTask> ListAll();
        IEnumerable<TdTask> ListOverdue();
        IEnumerable<TdTask> ListPending();
    }
}