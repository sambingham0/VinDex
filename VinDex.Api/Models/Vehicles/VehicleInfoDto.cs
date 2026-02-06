namespace VinDex.Api.Models.Vehicles;

public class VehicleInfoDto
{
    public required string Vin { get; set; }

    public required string Make { get; set; }
    public required string Model { get; set; }
    public required int Year { get; set; }

    public string? BodyStyle { get; set; }
    public string? VehicleType { get; set; }

    public EngineInfo? Engine { get; set; }
    public ManufacturingInfo? Manufacturing { get; set; }
}
