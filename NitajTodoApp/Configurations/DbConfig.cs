using Microsoft.EntityFrameworkCore;
using NitajTodoApp.Persistence;
using NitajTodoApp.Persistence.Interceptors;

namespace NitajTodoApp.Configurations;

public static class DbConfig
{
    public static IServiceCollection AddDbConfig(this IServiceCollection services)
    {
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(
        (serviceProvider, optionsBuilder) =>
        {
            var interceptor = serviceProvider.GetRequiredService<UpdateAuditableEntitiesInterceptor>();

            optionsBuilder
            .UseInMemoryDatabase("NitajTodoApp")
            .AddInterceptors(interceptor);
        });

        return services;
    }

}