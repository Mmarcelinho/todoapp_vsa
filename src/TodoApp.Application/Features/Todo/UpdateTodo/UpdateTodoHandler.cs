namespace TodoApp.Application.Features.Todo.UpdateTodo;

public record UpdateTodoCommand(Guid Id, string Name, bool IsCompleted) : IRequest<Unit>;

public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty()
            .MaximumLength(100)
            .WithMessage("Name is required!");
    }
}

internal class UpdateTodoHandler(TodoDbContext dbContext) : IRequestHandler<UpdateTodoCommand, Unit>
{
    public async Task<Unit> Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = await dbContext
            .TodoItems
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (todo is null) throw new NotFoundException($"Todo with Id {command.Id} not found!");

        todo.Name = command.Name;
        todo.IsCompleted = command.IsCompleted;
        todo.LastUpdatedOnUtc = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
