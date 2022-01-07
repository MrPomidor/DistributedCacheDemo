using Common;
using Common.InMemory;
using Common.Redis;
using HybridCacheApi;

var builder = WebApplication.CreateBuilder(args);

var redisConnectionString = builder.Configuration.GetSection("Redis").GetValue<string>("ConnectionString");
if (string.IsNullOrEmpty(redisConnectionString))
    throw new ApplicationException("Redis connection string missing in configuration");

var rabbitMqHostname = builder.Configuration.GetSection("RabbitMQ").GetValue<string>("HostName");
if (string.IsNullOrEmpty(rabbitMqHostname))
    throw new ApplicationException("RabbitMQ connection string missing in configuration");

builder.Services
    .AddCommon()
    .AddInMemoryCache()
    .AddDistributedCache(redisConnectionString)
    .AddHybridCache(rabbitMqHostname);

var app = builder.Build();

app.MapControllers();

app.Run();
