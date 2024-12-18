namespace TodoApp.Application.Features.Todo.DeleteTodo;

public class DeleteTodoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/todos/{id:guid}", async (Guid Id, ISender sender) =>
        {
            await sender.Send(new DeleteTodoCommand(Id));

            return Results.NoContent();
        })
        .WithName("DeleteTodo")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Todo By Id")
        .WithDescription("Delete Todo by Id");
    }
}
