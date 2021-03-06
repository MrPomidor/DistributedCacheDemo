using Common;
using Common.Redis;

var builder = WebApplication.CreateBuilder(args);

var redisConnectionString = builder.Configuration.GetSection("Redis").GetValue<string>("ConnectionString");
if (string.IsNullOrEmpty(redisConnectionString))
    throw new ApplicationException("Redis connection string missing in configuration");

builder.Services
    .AddCommon()
    .AddDistributedCache(redisConnectionString);

var app = builder.Build();

app.UseCommon();

app.Run();
