namespace TodoApp.Application.Features.Todo.GetTodoById;

public class GetTodoByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/todos/{id:guid}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new GetTodoByIdQuery(Id));

            return Results.Ok(result);
        })
        .WithName("GetTodoById")
        .Produces<GetTodoByIdResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Todo by Id")
        .WithDescription("Get Todo by Id");
    }
}
