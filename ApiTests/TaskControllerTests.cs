using API.Controllers;
using Data.Interfaces;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Reflection;

namespace ApiTests
{
    public class TaskControllerTests
    {
        private readonly TaskController _taskController;
        private readonly Mock<ITaskRepository> _repository = new();
        public TaskControllerTests()
        {
            _taskController = new TaskController(_repository.Object);
        }

        private readonly TdTask testTask = new TdTask()
        {
            Title = "Do something",
            DueDate = DateTime.Today.AddDays(2),
        };

        private readonly Guid testGuid = Guid.NewGuid();

        [Fact]
        public async Task Post_ValidTask_ReturnsCreatedResponse()
        {
            // Arrange
            var newTaskAfterCommit = new TdTask();
            // Because Id has private setter, it must be assigned in this way
            var Id = newTaskAfterCommit.GetType().GetProperty(nameof(TdTask.Id), BindingFlags.Public | BindingFlags.Instance);
            Id!.SetValue(newTaskAfterCommit, testGuid);

            // Mock object addition to a data repository
            _repository.Setup(m => m.AddAsync(testTask)).ReturnsAsync(newTaskAfterCommit);

            // Act
            var response = await _taskController.Post(testTask);

            // Assert
            Assert.IsType<CreatedAtActionResult>(response);
            var result = response as CreatedAtActionResult;
            Assert.Equal(testGuid, result?.Value);
        }

        [Fact]
        public async Task Get_ValidId_ReturnsOkResponse()
        {
            // Arrange
            // Mock object addition to a data repository
            _repository.Setup(m => m.GetByIdAsync(testGuid)).ReturnsAsync(testTask);

            // Act
            var response = await _taskController.Get(testGuid);

            // Assert
            Assert.IsType<OkObjectResult>(response);

            var result = response as OkObjectResult;
            Assert.IsType<TdTask>(result!.Value);
        }

        [Fact]
        public async Task Put_ValidTaskWithId_ReturnsOkResponse()
        {
            // Arrange
            // Mock object addition to a data repository
            _repository.Setup(m => m.UpdateAsync(testGuid, testTask));

            // Act
            var response = await _taskController.Put(testGuid, testTask);

            // Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task Delete_ValidId_ReturnsOkResponse()
        {
            // Arrange
            // Mock object addition to a data repository
            _repository.Setup(m => m.RemoveAsync(testGuid));

            // Act
            var response = await _taskController.Delete(testGuid);

            // Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task MarkComplete_ValidId_ReturnsOkResponse()
        {
            // Arrange
            _repository.Setup(m => m.SetCompletionStatusAsync(testGuid, true));

            // Act
            var response = await _taskController.MarkComplete(testGuid);

            // Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task MarkIncomplete_ValidId_ReturnsOkResponse()
        {
            // Arrange
            _repository.Setup(m => m.SetCompletionStatusAsync(testGuid, false));

            // Act
            var response = await _taskController.MarkIncomplete(testGuid);

            // Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public void ListAll_ReturnsOkResponse()
        {
            // Arrange
            _repository.Setup(m => m.ListAll()).Returns(new List<TdTask>() { testTask });

            // Act
            var response = _taskController.ListAll();

            // Assert
            Assert.IsType<OkObjectResult>(response);

            var result = response as OkObjectResult;
            Assert.IsAssignableFrom<IEnumerable<TdTask>>(result!.Value);
        }

        [Fact]
        public void ListOverdue_ReturnsOkResponse()
        {
            // Arrange
            _repository.Setup(m => m.ListOverdue()).Returns(new List<TdTask>() { testTask });

            // Act
            var response = _taskController.ListOverdue();

            // Assert
            Assert.IsType<OkObjectResult>(response);

            var result = response as OkObjectResult;
            Assert.IsAssignableFrom<IEnumerable<TdTask>>(result!.Value);
        }

        [Fact]
        public void ListPending_ReturnsOkResponse()
        {
            // Arrange
            _repository.Setup(m => m.ListOverdue()).Returns(new List<TdTask>() { testTask });

            // Act
            var response = _taskController.ListPending();

            // Assert
            Assert.IsType<OkObjectResult>(response);

            var result = response as OkObjectResult;
            Assert.IsAssignableFrom<IEnumerable<TdTask>>(result!.Value);
        }
    }
}