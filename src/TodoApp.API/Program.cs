var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options => options.AddDefaultPolicy(
        policy => policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "VSA Todo API", Version = "v1" }));

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails()
    .AddCarter();
    
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors();
app.MapCarter();
app.UseExceptionHandler(options => { });

await UpdateDatabase();

app.Run();

async Task UpdateDatabase()
{
    await using var scope = app.Services.CreateAsyncScope();

    await MigrateExtension.MigrateDatabase(scope.ServiceProvider);
}