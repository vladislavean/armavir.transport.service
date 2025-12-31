using armavir.transport.dal;
using armavir.transport.gateway.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureMapper();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDal(builder.Configuration);

var app = builder.Build();

app.MapOpenApi();

app.MapControllers();

app.Services.MigrateDb();

app.Run();