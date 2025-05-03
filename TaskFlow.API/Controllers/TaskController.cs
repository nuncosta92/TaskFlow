using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("It was not possible to obtain the user ID from the token.");
            }


            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                Status = request.Status
            };

            try
            {
                var createdTask = await _taskService.CreateTaskAsync(task);
                return Ok(createdTask);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the task.");
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetTasks()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = Guid.Parse(userIdClaim);

            var tasks = await _taskService.GetTasksAsync(userId);

            if (tasks == null || !tasks.Any())
            {
                return NotFound("No tasks found for this user.");
            }

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
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userId = Guid.Parse(userIdClaim);

                var updatedTask = await _taskService.UpdateTaskAsync(task, userId);

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
        [Authorize]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

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
