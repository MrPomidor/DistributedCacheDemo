﻿using Microsoft.Extensions.DependencyInjection;

namespace Common;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services
            .AddControllers();

        return services
            .AddTransient<ICache, DummyCache>()
            .AddSingleton<IDataFactory, DataFactory>();
    }

    public static IServiceCollection ReplaceTransient<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        return services.Replace<TService, TImplementation>(ServiceLifetime.Transient);
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
