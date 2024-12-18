namespace TodoApp.Application.Features.Todo.GetTodos;

public class GetTodosEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/todos", async (ISender sender) =>
        {
            var result = await sender.Send(new GetTodosQuery());

            return Results.Ok(result);
        })
        .WithName("GetTodos")
        .Produces<GetTodosResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Todos")
        .WithDescription("Get Todos");
    }
}
