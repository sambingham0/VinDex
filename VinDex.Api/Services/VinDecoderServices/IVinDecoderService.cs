using VinDex.Api.Models.Vehicles;

namespace VinDex.Api.Services;

public interface IVinDecoderService
{
    Task<VehicleInfoDto?> DecodeVinAsync(string vin);
}