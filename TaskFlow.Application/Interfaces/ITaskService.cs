
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task<IEnumerable<TaskItem>> GetTasksAsync(Guid userId);
        Task<TaskItem> UpdateTaskAsync(TaskItem? task);
        Task<bool> DeleteTaskAsync(Guid taskId);
    }
}
