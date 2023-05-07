using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using MoneyManager.Api.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Service.Mappers;
using Service.Helpers;
using Serilog;
using Serilog.Configuration;
using Microsoft.Extensions.Configuration;
using Serilog.Extensions.Logging;
using MoneyManager.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddJwtService(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddCustomServices();

// Logger
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
var app = builder.Build();
EnvironmentHelper.WebHostPath =
    app.Services.GetRequiredService<IWebHostEnvironment>().WebRootPath;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();
//app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
