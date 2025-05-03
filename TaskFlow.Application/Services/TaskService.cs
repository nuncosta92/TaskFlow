using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Data;

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


        public  async Task<TaskItem> UpdateTaskAsync(TaskItem task, Guid userId)
        {

            // First, find the task by ID
            var existingTask = await _context.Tasks
                .Where(t => t.Id == task.Id)
                .FirstOrDefaultAsync();

            if (existingTask == null)
            {
                throw new Exception("Task not found");
            }

            // Checks if the task belongs to the authenticated user
            if (existingTask.UserId != userId)
            {
                throw new Exception("You do not have permission to update this task.");
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;

            await _context.SaveChangesAsync();

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
