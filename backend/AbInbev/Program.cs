using AbInbev;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CrossCutting.Mapping;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using CrossCutting.ViewModels.Employers;
using Application.Commom;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddValidatorsFromAssemblyContaining<AddEmployerRequestValidator>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();
builder.Services.AddDbContext<DefaultContext>(options =>
{
    options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Infrastructure")
                );
                
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Permite qualquer origem
              .AllowAnyMethod()   // Permite qualquer método HTTP (GET, POST, PUT, DELETE)
              .AllowAnyHeader();  // Permite qualquer cabeçalho
    });
});

builder.Services.AddAuthenticationHandler(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program), typeof(Mapping));
builder.Services.AddInfrastructureIoC(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();

