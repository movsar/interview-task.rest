using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task Put(TdTask tdTask) {
            await _storage.TdTasks.Update(tdTask);
        }

        [HttpDelete("delete")]
        public async Task Delete(Guid id) {
            await _storage.TdTasks.Remove(id);
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
