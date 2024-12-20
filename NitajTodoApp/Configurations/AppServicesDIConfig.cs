using Scrutor;

namespace NitajTodoApp.Configurations;

public static class AppServicesDIConfig
{
    public static IServiceCollection AddAppServicesDIConfig(this IServiceCollection services)
    {
        services
            .Scan(
                selector => selector
                    .FromAssemblies(Persistence.AssemblyReference.Assembly)
                    .AddClasses(false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

        return services;
    }
}