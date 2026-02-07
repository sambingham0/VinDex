namespace VinDex.Api.Models.Vehicles;

public class EngineInfo
{
    // Basic engine specs
    public int? Cylinders { get; set; }
    public int? Horsepower { get; set; }
    public double? DisplacementLiters { get; set; }

    // Fuel and drivetrain
    public string? FuelType { get; set; }
    public string? TransmissionStyle { get; set; }
    public string? DriveType { get; set; }

    // Axles and GVWR
    public int? Axles { get; set; }
    public string? Gvwr { get; set; }
}
