using ClinicApp2.Services;
using ClinicApp2.Services.Interfaces;
using Serilog;
using Serilog.Events;
using ClickHouse.Client.ADO;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.File("log_2024_05_24.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSerilog();

builder.Services.AddScoped<IClinicsService, ClinicsService>();

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("ClickHouseConnection");
builder.Services.AddSingleton(new ClickHouseConnection(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.e
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
