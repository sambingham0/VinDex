using Microsoft.AspNetCore.Mvc;
using VinDex.Api.Services;

namespace VinDex.Api.Controllers;

[ApiController]
[Route("api/vin")]
public class VinController : ControllerBase
{
    private readonly VinDecoderService _vinService;

    public VinController(VinDecoderService vinService)
    {
        _vinService = vinService;
    }

    [HttpGet("{vin}")]
    public async Task<IActionResult> Decode(string vin)
    {
        var vehicle = await _vinService.DecodeVinAsync(vin);

        if (vehicle == null)
            return NotFound();

        return Ok(vehicle);
    }
}
