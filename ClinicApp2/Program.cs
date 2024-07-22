using ClickHouse.Client.ADO;
using ClinicApp2.Services;
using ClinicApp2.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
