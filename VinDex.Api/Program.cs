using VinDex.Api.Services;
using Microsoft.EntityFrameworkCore;
using VinDex.Api.Data;
using VinDex.Api.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<VinDecoderService>();
builder.Services.AddHttpClient<RecallService>();

// Add CORS policy
var allowedOrigins = new string[] { "http://localhost:4200", "https://localhost:4200" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Db context
builder.Services.AddDbContext<VinDexDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AngularPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
