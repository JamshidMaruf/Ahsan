using Ahsan.Data.Contexts;
using Ahsan.Service.Helpers;
using Ahsan.Service.Mappers;
using Ahsan.WebApi.Extensions;
using Ahsan.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<EmailVerification>();
builder.Services.AddCustomServices();

// Logger
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


var app = builder.Build();

EnvironmentHelper.WebHostPath = app.Services.GetRequiredService<IWebHostEnvironment>().WebRootPath;
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExeptionHandlerMiddleWare>();

app.UseAuthorization();

app.MapControllers();

app.Run();
