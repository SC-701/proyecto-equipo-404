using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.DA;
using DA.Repositorios;
using Flujo;
using DA;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Autorizacion.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Autenticacion
var tokenConfiguration = builder.Configuration.GetSection("Token").Get<TokenConfiguracion>();
var jwtIssuer = tokenConfiguration.Issuer;
var jwtAudience = tokenConfiguration.Audience;
var jwtKey = tokenConfiguration.key;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    }
    );

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

builder.Services.AddScoped<IVentaDA, VentaDA>();
builder.Services.AddScoped<IVentaFlujo, VentaFlujo>();

builder.Services.AddScoped<IDetalleVentaDA, DetalleVentaDA>();
builder.Services.AddScoped<IDetalleVentaFlujo, DetalleVentaFlujo>();

builder.Services.AddScoped<IReporteDA, ReporteDA>();
builder.Services.AddScoped<IReporteFlujo, ReporteFlujo>();

builder.Services.AddTransient<Autorizacion.Abstracciones.Flujo.IAutorizacionFlujo, Autorizacion.Flujo.AutorizacionFlujo>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.ISeguridadDA, Autorizacion.DA.SeguridadDA>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.IRepositorioDapper, Autorizacion.DA.Repositorios.RepositorioDapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AutorizacionClaims();
app.UseAuthorization();

app.MapControllers();

app.Run();
