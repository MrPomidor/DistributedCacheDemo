using Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommon();

var app = builder.Build();

app.MapControllers();

app.Run();
