using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinDex.Api.Data.Entities;
using VinDex.Api.Data.Repositories;
using VinDex.Api.Models.Garage;
using VinDex.Api.Models.Vehicles;
using VinDex.Api.Services;
using VinDex.Api.Services.Mappers;
using VinDex.Api.Utilities;

namespace VinDex.Api.Controllers;

[ApiController]
[Route("api/garage")]
[Authorize]
public class GarageController : ControllerBase
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IVinDecoderService _vinDecoderService;

    public GarageController(IVehicleRepository vehicleRepository, IVinDecoderService vinDecoderService)
    {
        _vehicleRepository = vehicleRepository;
        _vinDecoderService = vinDecoderService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GarageVehicleDto>>> GetGarage()
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        var savedVehicles = await _vehicleRepository.GetGarageEntriesByUserIdAsync(userId.Value);

        return Ok(savedVehicles.Select(entry => new GarageVehicleDto
        {
            SavedAt = entry.SavedAt,
            Vehicle = VehicleMapper.ToDto(entry.Vehicle)
        }));
    }

    [HttpPost]
    public async Task<ActionResult<SaveVehicleResponse>> SaveVehicle([FromBody] SaveVehicleRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Vin) || !VinUtils.IsValid(request.Vin))
        {
            return BadRequest(new { Message = "Invalid VIN format." });
        }

        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        var normalizedVin = VinUtils.Normalize(request.Vin);
        var decodedVehicle = await _vinDecoderService.DecodeVinAsync(normalizedVin);
        if (decodedVehicle == null)
        {
            return NotFound();
        }

        var vehicle = await _vehicleRepository.GetByVinAsync(normalizedVin);
        if (vehicle == null)
        {
            return NotFound();
        }

        var existingEntry = await _vehicleRepository.GetGarageEntryAsync(userId.Value, vehicle.Id);
        if (existingEntry != null)
        {
            return Ok(new SaveVehicleResponse
            {
                AlreadySaved = true,
                Vehicle = VehicleMapper.ToDto(vehicle)
            });
        }

        await _vehicleRepository.AddGarageEntryAsync(new UserVehicle
        {
            UserId = userId.Value,
            VehicleId = vehicle.Id
        });
        await _vehicleRepository.SaveChangesAsync();

        return Ok(new SaveVehicleResponse
        {
            AlreadySaved = false,
            Vehicle = decodedVehicle
        });
    }

    private int? GetUserId()
    {
        var userIdValue = User.FindFirstValue("id");
        return int.TryParse(userIdValue, out var userId) ? userId : null;
    }
}

public class SaveVehicleRequest
{
    public required string Vin { get; set; }
}

public class SaveVehicleResponse
{
    public required bool AlreadySaved { get; set; }
    public required VehicleInfoDto Vehicle { get; set; }
}