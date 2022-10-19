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
        public async Task<IActionResult> Post(TdTask tdTask) {
            await _storage.TdTasks.Add(tdTask);
            return Ok();
        }

        [HttpGet("retrieve")]
        public async Task<IActionResult> Get(Guid id) {
            var tdTask = await _storage.TdTasks.GetById(id);
            return Ok(tdTask);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put(Guid id, TdTask tdTask) {
            await _storage.TdTasks.Update(id, tdTask);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id) {
            await _storage.TdTasks.Remove(id);
            return Ok();
        }

        [HttpPatch("mark/complete")]
        public async Task<IActionResult> MarkComplete(Guid id) {
            await _storage.TdTasks.SetCompletionStatus(id, true);

            return Ok();
        }

        [HttpPatch("mark/incomplete")]
        public async Task<IActionResult> MarkIncomplete(Guid id) {
            await _storage.TdTasks.SetCompletionStatus(id, false);

            return Ok();
        }

        [HttpGet("list")]
        public IActionResult ListAll() {
            return Ok(_storage.TdTasks.ListAll());
        }

        [HttpGet("list/overdue")]
        public IActionResult ListOverdue() {
            return Ok(_storage.TdTasks.ListOverdue());
        }

        [HttpGet("list/pending")]
        public IActionResult ListPending() {
            return Ok(_storage.TdTasks.ListPending());
        }

    }
}
