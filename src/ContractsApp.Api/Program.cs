using ContractsApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

// Konfiguriere Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting Contracts WebApp API");

    var builder = WebApplication.CreateBuilder(args);

    // Serilog hinzufügen
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddControllers();

    // Entity Framework Core - In-Memory Database für MVP
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("ContractsDb"));

    // CORS-Policy für Frontend
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    // Swagger/OpenAPI
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() 
        { 
            Title = "Contracts API", 
            Version = "v1",
            Description = "API für Vertrags-Sonderkunden Verwaltung"
        });
    });

    var app = builder.Build();

    // Initialisiere Datenbank mit Seed-Daten
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
        Log.Information("Database initialized with seed data");
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contracts API V1");
            c.RoutePrefix = "swagger";
        });
    }

    app.UseHttpsRedirection();

    app.UseCors("AllowAll");

    app.UseAuthorization();

    app.MapControllers();

    Log.Information("API is running");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
