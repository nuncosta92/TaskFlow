using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.Application.Services
{
    class TaskService : ITaskService
    {
        private readonly List<TaskItem> _tasks = new(); // Temporary in-memory list

        public async Task<TaskItem> CreateTaskAsync(string title, string description, Guid userId)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Status = TaskStatus.Pending
            };

            _tasks.Add(task);

            return await Task.FromResult(task);
        }

        public async Task<IEnumerable<TaskItem>> GetTasksAsync(Guid userId)
        {
            var userTaks = _tasks.Where(t => t.UserId == userId).ToList();
            return await Task.FromResult<IEnumerable<TaskItem>>(userTaks);
        }

        public  async Task<TaskItem> UpdateTaskAsync(Guid taskId, string title, string description, TaskStatus status)
        {
            var task = _tasks.SingleOrDefault(t => t.Id == taskId);

            if (task == null)
            {
                throw new Exception("Task not found");
            }

            task.Title = title;
            task.Description = description;
            task.Status = status;

            return await Task.FromResult(task);
        }

        public async Task<bool> DeleteTaskAsync(Guid taskId)
        {
            var task = _tasks.SingleOrDefault(t => t.Id == taskId);
            
            if (task == null)
            {
                throw new Exception("Task not found");
            }

            _tasks.Remove(task);
            return await Task.FromResult(true);
        }
    }
}
