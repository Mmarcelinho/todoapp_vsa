namespace TodoApp.Application.Features.Todo.GetTodoById;

public record GetTodoByIdQuery(Guid Id) : IRequest<GetTodoByIdResult>;
public record GetTodoByIdResult(Guid Id, string Name, bool IsCompleted);

internal class GetTodoByIdQueryHandler(TodoDbContext dbContext) : IRequestHandler<GetTodoByIdQuery, GetTodoByIdResult>
{
    public async Task<GetTodoByIdResult> Handle(GetTodoByIdQuery query, CancellationToken cancellationToken)
    {
        var todo = await dbContext
            .TodoItems
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (todo is null) throw new NotFoundException($"Todo with Id {query.Id} not found!");

        return new GetTodoByIdResult(todo.Id, todo.Name, todo.IsCompleted);
    }
}
