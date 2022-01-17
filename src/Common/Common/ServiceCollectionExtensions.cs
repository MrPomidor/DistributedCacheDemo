using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace Common;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services
            .AddControllers();

        services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();

            options.MimeTypes = ResponseCompressionDefaults.MimeTypes;
        });

        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest;
        });

        return services
            .AddSingleton<IInstanceIdentifierProvider, InstanceIdentifierProvider>()
            .AddSingleton<ISerializer, NewtonsoftSerializer>()
            .AddSingleton<ICache, DummyCache>()
            .AddSingleton<IDataFactory, DataFactory>();
    }

    public static IServiceCollection ReplaceSingletone<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        return services.Replace<TService, TImplementation>(ServiceLifetime.Singleton);
    }

    public static IServiceCollection Replace<TService, TImplementation>(
        this IServiceCollection services,
        ServiceLifetime lifetime)
        where TService : class
        where TImplementation : class, TService
    {
        var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
        if (descriptorToRemove != null)
        {
            services.Remove(descriptorToRemove);
        }

        var descriptorToAdd = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);

        services.Add(descriptorToAdd);

        return services;
    }
}
