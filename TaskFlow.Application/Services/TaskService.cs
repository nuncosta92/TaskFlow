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
    public class TaskService : ITaskService
    {
        private readonly List<TaskItem> _tasks = new(); // Temporary in-memory list

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            _tasks.Add(task);
            return await Task.FromResult(task);
        }

        public async Task<IEnumerable<TaskItem>> GetTasksAsync(Guid userId)
        {
            var userTaks = _tasks.Where(t => t.UserId == userId).ToList();
            return await Task.FromResult<IEnumerable<TaskItem>>(userTaks);
        }


        public  async Task<TaskItem> UpdateTaskAsync(TaskItem task)
        {
            var taskUpdate = _tasks.SingleOrDefault(t => t.Id == task.Id);

            if (task == null)
            {
                throw new Exception("Task not found");
            }

            taskUpdate.Title = task.Title;
            taskUpdate.Description = task.Description;
            taskUpdate.Status = task.Status;

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
