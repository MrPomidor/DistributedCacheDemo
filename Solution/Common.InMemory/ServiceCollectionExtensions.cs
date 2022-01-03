using Microsoft.Extensions.DependencyInjection;

namespace Common.InMemory;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.ReplaceTransient<ICache, InMemoryCache>();

        return services;
    }
}
