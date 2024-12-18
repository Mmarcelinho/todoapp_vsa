namespace TodoApp.Application.Features.Todo.CreateTodo;

public record CreateTodoRequest(string Name);
public record CreateTodoResponse(Guid Id);

public class CreateTodoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/todos", async (CreateTodoRequest request, ISender sender) =>
        {
            var result = await sender.Send(new CreateTodoCommand(request.Name));

            var response = new CreateTodoResponse(result.Id);

            return Results.Created($"/todos/{response.Id}", response);
        })
        .WithName("CreateTodo")
        .Produces<CreateTodoResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Todo")
        .WithDescription("Create Todo");
    }
}
