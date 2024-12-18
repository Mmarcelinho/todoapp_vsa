namespace TodoApp.Application.Infrastructure.Persistence.Extensions;

public static class MigrateExtension
{
    public async static Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<TodoDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}