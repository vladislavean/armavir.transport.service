using armavir.transport.dal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDal(builder.Configuration);

var app = builder.Build();

app.MapOpenApi();

app.MapControllers();

app.Services.MigrateDb();

app.Run();