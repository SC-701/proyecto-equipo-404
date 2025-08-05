using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using DA;
using DA.Repositorios;
using Flujo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();

builder.Services.AddScoped<IEstadoDA, EstadoDA>();
builder.Services.AddScoped<IEstadoFlujo, EstadoFlujo>();

builder.Services.AddScoped<ITipoPlatilloDA, TipoPlatilloDA>();
builder.Services.AddScoped<ITipoPlatilloFlujo, TipoPlatilloFlujo>();

builder.Services.AddScoped<IPlatilloDA, PlatilloDA>();
builder.Services.AddScoped<IPlatilloFlujo, PlatilloFlujo>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
