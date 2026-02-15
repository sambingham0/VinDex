using Microsoft.AspNetCore.Mvc;
using VinDex.Api.Services;
using VinDex.Api.Models.Recalls;
using VinDex.Api.Utilities;

namespace VinDex.Api.Controllers;

[ApiController]
[Route("api/vin")]
public class VinController : ControllerBase
{
    private readonly IVinDecoderService _vinService;
    private readonly IRecallService _recallService;

    public VinController(IVinDecoderService vinService, IRecallService recallService)
    {
        _vinService = vinService;
        _recallService = recallService;
    }

    [HttpGet("{vin}")]
    public async Task<IActionResult> Decode(string vin, [FromQuery] bool WRecalls = false)
    {
        if (!VinUtils.IsValid(vin))
        {
            return BadRequest("Invalid VIN format. Must be 17 alphanumeric characters.");
        }

        var normalizedVin = VinUtils.Normalize(vin);

        var vehicle = await _vinService.DecodeVinAsync(normalizedVin);
        if (vehicle == null)
            return NotFound();

        List<RecallDto>? recalls = null;
        if (WRecalls &&
            !string.IsNullOrWhiteSpace(vehicle.Make) &&
            !string.IsNullOrWhiteSpace(vehicle.Model) &&
            vehicle.Year > 0)
        {
            recalls = await _recallService.DecodeRecallAsync(vehicle.Make, vehicle.Model, vehicle.Year);
        }

        if (WRecalls)
            return Ok(new { vehicle, recalls });

        return Ok(vehicle);
    }
}
