using Microsoft.Extensions.Caching.Memory;
using VinDex.Api.Models.Recalls;

namespace VinDex.Api.Services;

public class CachedRecallService : IRecallService
{
    private readonly IRecallService _inner;
    private readonly IMemoryCache _cache;

    public CachedRecallService(IRecallService inner, IMemoryCache cache)
    {
        _inner = inner;
        _cache = cache;
    }

    public async Task<List<RecallDto>> DecodeRecallAsync(string make, string model, int year)
    {
        var key = $"recalls_{make}_{model}_{year}".ToLowerInvariant();
        var recalls = await _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromHours(6));
            return await _inner.DecodeRecallAsync(make, model, year);
        });

        return recalls ?? new List<RecallDto>();
    }
}
