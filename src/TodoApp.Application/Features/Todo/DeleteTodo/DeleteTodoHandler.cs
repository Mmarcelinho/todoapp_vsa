namespace TodoApp.Application.Features.Todo.DeleteTodo;

public record DeleteTodoCommand(Guid Id) : IRequest<Unit>;

public class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteTodoCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required!");
    }
}

internal class DeleteTodoCommandHandler(TodoDbContext dbContext) : IRequestHandler<DeleteTodoCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = await dbContext
            .TodoItems
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (todo is null) throw new NotFoundException($"Todo with Id {command.Id} not found!");

        dbContext.TodoItems.Remove(todo);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}