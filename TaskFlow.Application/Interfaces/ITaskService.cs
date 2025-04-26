
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskItem> CreateTaskAsync(string title, string description, Guid userId);
        Task<IEnumerable<TaskItem>> GetTasksAsync(Guid userId);
        Task<TaskItem> UpdateTaskAsync(Guid taskId, string title, string description, TaskStatus status);
        Task<bool> DeleteTaskAsync(Guid taskId);
    }
}
