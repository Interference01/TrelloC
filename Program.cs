using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TrelloC.Authorization;
using TrelloC.Core;
using TrelloC.Helpers;
using TrelloC.Logging;
using TrelloC.Middlewares;
using TrelloC.Models.Entities;
using TrelloC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<DataContext>();

builder.Services.AddControllers()
       .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<LoggingCustomSettings>(builder.Configuration.GetSection("LoggingCustomSettings"));

// configure DI for application services
builder.Services.AddScoped<IHttpLogger, HttpLoggerFile>();
builder.Services.AddScoped<IHttpLogger, HttpLoggerConsole>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dataContext.Database.Migrate();
}

app.UseHttpsRedirection();
// global cors policy
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();
// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();
// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
