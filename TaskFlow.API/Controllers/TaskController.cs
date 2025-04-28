using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.Models;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskRequest request)
        {
            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId,
                Status = request.Status
            };

            try
            {
                var createdTask = await _taskService.CreateTaskAsync(task);
                return Ok(createdTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetTasks(Guid userId)
        {
            var tasks = await _taskService.GetTasksAsync(userId);
            return Ok(tasks);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTask(UpdateTaskRequest request)
        {
            var task = new TaskItem
            {
                Id = request.TaskId,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status
            };

            try
            {
                var updatedTask = await _taskService.UpdateTaskAsync(task);

                if (updatedTask == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            try
            {
                var result = await _taskService.DeleteTaskAsync(taskId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
