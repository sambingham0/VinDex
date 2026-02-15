using Microsoft.Extensions.Caching.Memory;
using VinDex.Api.Models.Vehicles;

namespace VinDex.Api.Services;

public class CachedVinDecoderService : IVinDecoderService
{
    private readonly IVinDecoderService _inner;
    private readonly IMemoryCache _cache;

    public CachedVinDecoderService(IVinDecoderService inner, IMemoryCache cache)
    {
        _inner = inner;
        _cache = cache;
    }

    public async Task<VehicleInfoDto?> DecodeVinAsync(string vin)
    {
        var key = $"vehicle_{vin}".ToLowerInvariant();
        return await _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromHours(24));
            return await _inner.DecodeVinAsync(vin);
        });
    }
}