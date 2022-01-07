using Microsoft.Extensions.DependencyInjection;

namespace Common.InMemory;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryCache(this IServiceCollection services)
    {
        services
            .AddMemoryCache()
            .AddSingleton<InMemoryCache>()
            .ReplaceSingletone<ICache, InMemoryCache>();

        return services;
    }
}
