namespace TodoApp.Application.Features.Todo.GetTodos;

public record GetTodosQuery() : IRequest<IEnumerable<GetTodosResult>>;
public record GetTodosResult(Guid Id, string Name, bool IsCompleted);

internal class GetTodosQueryHandler(TodoDbContext dbContext) : IRequestHandler<GetTodosQuery, IEnumerable<GetTodosResult>>
{
    public async Task<IEnumerable<GetTodosResult>> Handle(GetTodosQuery query, CancellationToken cancellationToken)
    {
        var todos = await dbContext
            .TodoItems
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = todos.Select(todo =>
            new GetTodosResult(todo.Id, todo.Name, todo.IsCompleted));

        return result;
    }
}
