using Microsoft.EntityFrameworkCore;
using Tourify.Core.Interfaces;
using Tourify.Infrastructure.Data;
using Tourify.Infrastructure.Repositories;
using Tourify.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TourifyContext>(options =>
    options.UseMySql(connectionString,
        ServerVersion.Create(8, 1, 0, Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql)));

// Register repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Event Service Registration
builder.Services.AddHttpClient<IEventService, EventService>();

// CORS configuration - Daha esnek hale getirdik
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins(allowedOrigins) // appsettings.json'dan gelen orijinler
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware sýralamasý önemli
app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin"); // CORS middleware'i burada
app.UseAuthorization();
app.MapControllers();

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TourifyContext>();
    await context.Database.MigrateAsync();
    await SeedData.SeedAsync(context);
}

app.Run();