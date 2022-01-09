using Microsoft.Extensions.DependencyInjection;

namespace Common.HybridCache;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHybridCache(this IServiceCollection services)
    {
        return services
            .AddSingleton<HybridCache>()
            .ReplaceSingletone<ICache, HybridCache>();
    }
}
