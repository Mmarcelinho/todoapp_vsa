namespace TodoApp.Application.Domain.Todo;

public class TodoItem
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsCompleted { get; set; } = false;

    public DateTime LastUpdatedOnUtc { get; set; } 
}
