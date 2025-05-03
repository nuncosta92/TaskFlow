using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Data;
using TaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly List<TaskItem> _tasks = new(); // Temporary in-memory list
        private readonly TaskFlowDbContext _context;

        public TaskService(TaskFlowDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {

            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksAsync(Guid userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
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

            _context.Update(taskUpdate);

            return task;
        }

        public async Task<bool> DeleteTaskAsync(Guid taskId)
        {
            var task = _tasks.SingleOrDefault(t => t.Id == taskId);
            
            if (task == null)
            {
                throw new Exception("Task not found");
            }

            _context.Remove(task);

            return true;
        }

    }
}
