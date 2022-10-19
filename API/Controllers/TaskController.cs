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
        [HttpGet]
        public IEnumerable<TdTask> Get() {
            return _storage.TdTasks.GetAll();
        }

        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        [HttpPost]
        public async Task Post([FromBody] string value) {
            await _storage.TdTasks.Add(new TdTask() { Title = "Hi there!" });
        }

        //// PUT api/<TaskController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value) {
        //}

        //// DELETE api/<TaskController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id) {
        //}
    }
}
