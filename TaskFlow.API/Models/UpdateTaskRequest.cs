using TaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.API.Models
{
    public class UpdateTaskRequest
    {
        public Guid TaskId { get; set; }  // Task we want to update
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Pending; //Default value
    }
}
