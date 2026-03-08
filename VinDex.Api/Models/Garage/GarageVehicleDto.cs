using VinDex.Api.Models.Vehicles;

namespace VinDex.Api.Models.Garage;

public class GarageVehicleDto
{
    public required DateTime SavedAt { get; set; }
    public required VehicleInfoDto Vehicle { get; set; }
}