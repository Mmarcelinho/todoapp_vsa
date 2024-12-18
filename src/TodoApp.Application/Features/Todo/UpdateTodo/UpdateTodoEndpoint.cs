namespace TodoApp.Application.Features.Todo.UpdateTodo;

public record UpdateTodoRequest(Guid Id, string Name, bool IsCompleted);

public class UpdateTodoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/todos/{id:guid}", async (Guid Id, UpdateTodoRequest request, ISender sender) =>
        {
            await sender.Send(new UpdateTodoCommand(request.Id, request.Name, request.IsCompleted));

            return Results.NoContent();
        })
        .WithName("UpdateTodo")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Todo By Id")
        .WithDescription("Update Todo by Id");
    }
}

