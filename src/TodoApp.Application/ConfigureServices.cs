namespace TodoApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            options.AddOpenBehavior(typeof(LoggingBehavior<,>));
            options.AddOpenBehavior(typeof(PerformanceBehavior<,>));
        });

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            services.AddDbContext<TodoDbContext>(options =>
                options.UseInMemoryDatabase("TodoList"));
        else
        {
            var connectionString = configuration.GetConnectionString("Connection") ??
            throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<TodoDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }

        return services;
    }
}
