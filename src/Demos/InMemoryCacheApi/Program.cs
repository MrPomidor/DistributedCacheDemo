using Common;
using Common.InMemory;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCommon()
    .AddInMemoryCache();

var app = builder.Build();

app.UseCommon();

app.Run();
