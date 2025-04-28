using TaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.API.Models
{
    public class CreateTaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid UserId { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Pending; //Default value
    }
}
