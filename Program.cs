using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TasksAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Logging detallado
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Servicios básicos
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Middleware básico
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

// Health check básico
app.MapHealthChecks("/health");

// Manejo de errores global
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        
        logger.LogError(exceptionHandlerPathFeature?.Error, 
            "Error no manejado en la aplicación");
        
        await context.Response.WriteAsJsonAsync(new { 
            error = "Se produjo un error interno",
            detail = exceptionHandlerPathFeature?.Error?.Message
        });
    });
});

// Ruta de diagnóstico
app.MapGet("/diagnostic", (ILogger<Program> logger) =>
{
    logger.LogInformation("Diagnostic endpoint called");
    return new
    {
        timestamp = DateTime.UtcNow,
        environment = app.Environment.EnvironmentName,
        isProduction = app.Environment.IsProduction(),
        isDevelopment = app.Environment.IsDevelopment()
    };
});

app.MapControllers();

app.Run();
