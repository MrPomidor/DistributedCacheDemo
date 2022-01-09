using Common;
using Common.InMemory;
using Common.Redis;
using HybridCacheApi.Redis;

var builder = WebApplication.CreateBuilder(args);

var redisConnectionString = builder.Configuration.GetSection("Redis").GetValue<string>("ConnectionString");
if (string.IsNullOrEmpty(redisConnectionString))
    throw new ApplicationException("Redis connection string missing in configuration");

builder.Services
    .AddCommon()
    .AddInMemoryCache()
    .AddDistributedCache(redisConnectionString)
    .AddRedisHybridCache();

var app = builder.Build();

app.MapControllers();

app.Run();
