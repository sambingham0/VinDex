using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using Microsoft.IdentityModel.Tokens;
using VinDex.Api.Data.Entities;
using VinDex.Api.Data.Repositories;

namespace VinDex.Api.Services;

public class AuthService
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository repository, IConfiguration config)
    {
        _repository = repository;
        _config = config;
    }

    public async Task<string> LoginWithGoogleAsync(string googleCredential)
    {
        if (string.IsNullOrWhiteSpace(googleCredential))
        {
            throw new ArgumentException("Google credential is required.", nameof(googleCredential));
        }

        var googleClientId = _config["Auth:GoogleClientId"];
        if (string.IsNullOrWhiteSpace(googleClientId))
        {
            throw new InvalidOperationException("Auth:GoogleClientId is not configured.");
        }

        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = [googleClientId]
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(googleCredential, settings);

        var user = await _repository.GetBySubjectIdAsync(payload.Subject);
        if (user == null)
        {
            user = new User
            {
                Email = payload.Email,
                Name = payload.Name,
                GoogleSubjectId = payload.Subject
            };
            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();
        }

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _config.GetSection("Auth");
        var signingKey = jwtSettings["JwtSigningKey"];
        if (string.IsNullOrWhiteSpace(signingKey))
        {
            throw new InvalidOperationException("Auth:JwtSigningKey is not configured.");
        }

        if (!double.TryParse(jwtSettings["JwtExpirationMinutes"], out var expirationMinutes))
        {
            throw new InvalidOperationException("Auth:JwtExpirationMinutes is not a valid number.");
        }

        var key = Encoding.UTF8.GetBytes(signingKey);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("id", user.Id.ToString()),
            new Claim("name", user.Name ?? "")
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
            Issuer = jwtSettings["JwtIssuer"],
            Audience = jwtSettings["JwtAudience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}