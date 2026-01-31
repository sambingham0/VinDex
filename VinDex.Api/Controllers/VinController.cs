using Microsoft.AspNetCore.Mvc;
using VinDex.Api.Services;
using Microsoft.Extensions.Caching.Memory;
using VinDex.Api.Models.Recalls;

namespace VinDex.Api.Controllers;

[ApiController]
[Route("api/vin")]
public class VinController : ControllerBase
{
    private readonly VinDecoderService _vinService;
    private readonly RecallService _recallService;
    private readonly IMemoryCache _cache;

    public VinController(VinDecoderService vinService, RecallService recallService, IMemoryCache cache)
    {
        _vinService = vinService;
        _recallService = recallService;
        _cache = cache;
    }

    [HttpGet("{vin}")]
    public async Task<IActionResult> Decode(string vin, [FromQuery] bool WRecalls = false)
    {
        var vehicle = await _cache.GetOrCreateAsync(vin.ToUpperInvariant(), _ => _vinService.DecodeVinAsync(vin));
        if (vehicle == null)
            return NotFound();

        List<RecallDto>? recalls = null;
        if (WRecalls &&
            !string.IsNullOrWhiteSpace(vehicle.Make) &&
            !string.IsNullOrWhiteSpace(vehicle.Model) &&
            vehicle.Year > 0)
        {
            recalls = await _recallService.DecodeRecallAsync(
                vehicle.Make, vehicle.Model, vehicle.Year);
        }

        if (WRecalls)
            return Ok(new { vehicle, recalls });

        return Ok(vehicle);
    }
}
