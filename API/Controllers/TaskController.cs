using Api.ActionFilters;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _repository;
        public TaskController(ITaskRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("create")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Post(TdTask tdTask)
        {
            var createdTask = await _repository.AddAsync(tdTask);
            return CreatedAtAction(nameof(Get), createdTask.Id);
        }

        [HttpGet("retrieve")]
        public async Task<IActionResult> Get(Guid id)
        {
            var tdTask = await _repository.GetByIdAsync(id);
            return Ok(tdTask);
        }

        [HttpPut("update")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Put(Guid id, TdTask tdTask)
        {
            await _repository.UpdateAsync(id, tdTask);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.RemoveAsync(id);
            return Ok();
        }

        [HttpPatch("mark/complete")]
        public async Task<IActionResult> MarkComplete(Guid id)
        {
            await _repository.SetCompletionStatusAsync(id, true);

            return Ok();
        }

        [HttpPatch("mark/incomplete")]
        public async Task<IActionResult> MarkIncomplete(Guid id)
        {
            await _repository.SetCompletionStatusAsync(id, false);

            return Ok();
        }

        [HttpGet("list")]
        public IActionResult ListAll()
        {
            return Ok(_repository.ListAll());
        }

        [HttpGet("list/overdue")]
        public IActionResult ListOverdue()
        {
            return Ok(_repository.ListOverdue());
        }

        [HttpGet("list/pending")]
        public IActionResult ListPending()
        {
            return Ok(_repository.ListPending());
        }

    }
}
