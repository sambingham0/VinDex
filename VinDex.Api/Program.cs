using VinDex.Api.Services;
using Microsoft.EntityFrameworkCore;
using VinDex.Api.Data;
using VinDex.Api.Data.Repositories;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<VinDecoderService>()
    .AddStandardResilienceHandler();
builder.Services.AddHttpClient<RecallService>()
    .AddStandardResilienceHandler();

builder.Services.AddScoped<IVinDecoderService>(provider => 
    new CachedVinDecoderService(
        provider.GetRequiredService<VinDecoderService>(),
        provider.GetRequiredService<IMemoryCache>()
    ));

builder.Services.AddScoped<IRecallService>(provider => 
    new CachedRecallService(
        provider.GetRequiredService<RecallService>(),
        provider.GetRequiredService<IMemoryCache>()
    ));

// Add CORS policy
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? [];

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

if (!app.Environment.IsDevelopment())
{
    // app.UseHttpsRedirection(); // Disable in containerized/production unless SSL is configured
}
else
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseCors("AngularPolicy");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<VinDexDbContext>();
    context.Database.Migrate();
}

app.Run();
