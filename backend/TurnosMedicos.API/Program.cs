using Microsoft.EntityFrameworkCore;
using TurnosMedicos.API.Middleware;
using TurnosMedicos.Application.Interfaces.Repositories;
using TurnosMedicos.Application.Interfaces.Services;
using TurnosMedicos.Application.Services;
using TurnosMedicos.Application.Settings;
using TurnosMedicos.Infrastructure.Data;
using TurnosMedicos.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=turnos.db"));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddScoped<ITurnosService, TurnosService>();
builder.Services.AddScoped<ITurnosRepository, TurnosRepository>();

builder.Services.AddScoped<IMedicosService, MedicosService>();
builder.Services.AddScoped<IMedicosRepository, MedicosRepository>();

builder.Services.AddScoped<IPacientesService, PacientesService>();
builder.Services.AddScoped<IPacientesRepository, PacientesRepository>();

builder.Services.AddScoped<ISucursalesService, SucursalesService>();
builder.Services.AddScoped<ISucursalesRepository, SucursalesRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<BusinessRulesSettings>(
    builder.Configuration.GetSection("BusinessRules"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();