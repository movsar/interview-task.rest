using Api.ActionFilters;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _repository;
        public TaskController(ITaskRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates a new Task
        /// </summary>
        /// <response code="201">Returns the newly created task id</response>
        [HttpPost("Create")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Post(TdTask tdTask)
        {
            var createdTask = await _repository.AddAsync(tdTask);
            return CreatedAtAction(nameof(Get), createdTask.Id);
        }

        /// <summary>
        /// Retrieves a Task
        /// </summary>
        /// <response code="201">Returns the specified by its id task</response>
        [HttpGet("Read")]
        public async Task<IActionResult> Get(Guid id)
        {
            var tdTask = await _repository.GetByIdAsync(id);
            return Ok(tdTask);
        }

        /// <summary>
        /// Updates a Task
        /// </summary>
        /// <response code="201">Returns Ok</response>
        [HttpPut("Update")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Put(Guid id, TdTask tdTask)
        {
            await _repository.UpdateAsync(id, tdTask);
            return Ok();
        }

        /// <summary>
        /// Deletes a Task
        /// </summary>
        /// <response code="201">Returns Ok</response>
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.RemoveAsync(id);
            return Ok();
        }

        /// <summary>
        /// Marks a Task as completed
        /// </summary>
        /// <response code="201">Returns Ok</response>
        [HttpPatch("Mark/Complete")]
        public async Task<IActionResult> MarkComplete(Guid id)
        {
            await _repository.SetCompletionStatusAsync(id, true);

            return Ok();
        }

        /// <summary>
        /// Marks a Task as not completed
        /// </summary>
        /// <response code="201">Returns Ok</response>
        [HttpPatch("Mark/Incomplete")]
        public async Task<IActionResult> MarkIncomplete(Guid id)
        {
            await _repository.SetCompletionStatusAsync(id, false);

            return Ok();
        }

        /// <summary>
        /// Lists all tasks
        /// </summary>
        /// <response code="201">Returns a list of all tasks</response>
        [HttpGet("List")]
        public IActionResult ListAll()
        {
            return Ok(_repository.ListAll());
        }

        /// <summary>
        /// Lists overdue tasks
        /// </summary>
        /// <response code="201">Returns a list of overdue tasks</response>
        [HttpGet("List/overdue")]
        public IActionResult ListOverdue()
        {
            return Ok(_repository.ListOverdue());
        }

        /// <summary>
        /// Lists pending tasks
        /// </summary>
        /// <response code="201">Returns a list of pending tasks</response>
        [HttpGet("List/pending")]
        public IActionResult ListPending()
        {
            return Ok(_repository.ListPending());
        }

    }
}
