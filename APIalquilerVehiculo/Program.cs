using APIalquilerVehiculo.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("***** Configurando Servicios *******");

//importante agregar los paquetes JWT Y SWager
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };

    });
builder.Services.AddSwaggerGen(a =>
{
    a.OperationFilter<SecurityRequirementsOperationFilter>();
    a.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Autorizacion: Usar Bearer. Ejemplo \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});

builder.Services.AddCors();

// Add services to the container.*
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AplicationData>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

Console.WriteLine("***** Finalizado configuración de servicios *******");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

// PARA Acceso a cualquier frontend
app.UseCors(x => x.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());

app.MapControllers();

var scope = app.Services.CreateScope();
await Migrations(scope.ServiceProvider);

app.Run();

async Task Migrations(IServiceProvider serviceProvider)
{
    var context = serviceProvider.GetService<AplicationData>();
    var conn_appdb = context.Database.GetDbConnection();

    Console.WriteLine($"Conexion actual AppDB: {conn_appdb.ToString()} {Environment.NewLine} {conn_appdb.ConnectionString}");
    Console.WriteLine("********** Probando acceso ******************");

    try
    {
        Console.WriteLine("Base de datos disponible: " + context.Database.CanConnect()); ;
    }
    catch (Exception ex)
    {
        Console.Write($"Error al conectar: {ex.Message}");
    }
}