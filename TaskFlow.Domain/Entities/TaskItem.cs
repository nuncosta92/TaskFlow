namespace TaskFlow.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Pending;

        // FK - ligação ao User
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
