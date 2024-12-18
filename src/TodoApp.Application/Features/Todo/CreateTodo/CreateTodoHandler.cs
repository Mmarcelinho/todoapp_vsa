namespace TodoApp.Application.Features.Todo.CreateTodo;

public record CreateTodoCommand(string Name) : IRequest<CreateTodoResult>;
public record CreateTodoResult(Guid Id);

public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required!");
    }
}

internal class CreateTodoCommandHandler(TodoDbContext dbContext) : IRequestHandler<CreateTodoCommand, CreateTodoResult>
{
    public async Task<CreateTodoResult> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = new TodoItem
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            LastUpdatedOnUtc = DateTime.UtcNow
        };

        dbContext.TodoItems.Add(todo);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateTodoResult(todo.Id);
    }
}
