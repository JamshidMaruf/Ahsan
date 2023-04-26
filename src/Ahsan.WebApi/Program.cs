using Ahsan.Data.Contexts;
using Ahsan.Service.Helpers;
using Ahsan.Service.Mappers;
using Ahsan.WebApi.Extensions;
using Ahsan.WebApi.Helpers;
using Ahsan.WebApi.Middlewares;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<EmailVerification>();
builder.Services.AddCustomServices();

// Swagger setup
builder.Services.AddSwaggerService();

// Jwt services
builder.Services.AddJwtService(builder.Configuration);

// Logger
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//Convert  Api url name to dash case 
builder.Services.AddControllers(options =>
    options.Conventions.Add(
        new RouteTokenTransformerConvention(new RouteConfiguration())));

var app = builder.Build();

EnvironmentHelper.WebHostPath = 
    app.Services.GetRequiredService<IWebHostEnvironment>().WebRootPath;
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExeptionHandlerMiddleWare>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
