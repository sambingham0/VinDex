using VinDex.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

builder.Services.AddScoped<AuthService>();

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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("Auth");
    var signingKey = jwtSettings["JwtSigningKey"];
    if (string.IsNullOrWhiteSpace(signingKey))
    {
        throw new InvalidOperationException("Auth:JwtSigningKey is not configured.");
    }

    var key = Encoding.UTF8.GetBytes(signingKey);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["JwtIssuer"],
        ValidAudience = jwtSettings["JwtAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.FromMinutes(1)
    };
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// Db context
builder.Services.AddDbContext<VinDexDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<VinDexDbContext>();
    context.Database.Migrate();
}

app.Run();
