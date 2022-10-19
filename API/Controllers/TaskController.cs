using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase {
        private readonly Storage _storage;
        public TaskController(Storage storage) {
            _storage = storage;
        }

        [HttpPost("create")]
        public async Task Post(TdTask tdTask) {
            await _storage.TdTasks.Add(tdTask);
        }

        [HttpGet("retrieve")]
        public async Task<TdTask?> Get(Guid? id) {
            return await _storage.TdTasks.GetById(id);
        }

        [HttpPut("update")]
        public async Task Put(Guid? id, TdTask tdTask) {
            await _storage.TdTasks.Update(id, tdTask);
        }

        [HttpDelete("delete")]
        public async Task Delete(Guid id) {
            await _storage.TdTasks.Remove(id);
        }

        [HttpGet("list")]
        public IEnumerable<TdTask> ListAll() {
            return _storage.TdTasks.ListAll();
        }

        [HttpGet("list/overdue")]
        public IEnumerable<TdTask> ListOverdue() {
            return _storage.TdTasks.ListOverdue();
        }
        
        [HttpGet("list/pending")]
        public IEnumerable<TdTask> ListPending() {
            return _storage.TdTasks.ListPending();
        }

    }
}
