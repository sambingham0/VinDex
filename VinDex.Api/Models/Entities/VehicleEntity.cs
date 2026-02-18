using System.ComponentModel.DataAnnotations;

namespace VinDex.Api.Data.Entities;

public class Vehicle
{
    [Key]
    public int Id { get; set; }

    // Basic Vehicle Identity
    public string Vin { get; set; } = null!;
    public required string Make { get; set; }
    public required string Model { get; set; }
    public required int Year { get; set; }
    public string? Trim { get; set; }
    public string? Series { get; set; }
    public string? BodyStyle { get; set; }
    public string? VehicleType { get; set; }
    public string? Doors { get; set; }

    // Engine / Drivetrain
    public int? EngineCylinders { get; set; }
    public int? Horsepower { get; set; }
    public double? DisplacementLiters { get; set; }
    public string? FuelType { get; set; }
    public string? TransmissionStyle { get; set; }
    public string? DriveType { get; set; }
    public string? Axles { get; set; }
    public string? Gvwr { get; set; }

    // Manufacturing Info
    public string? PlantCountry { get; set; }
    public string? PlantState { get; set; }
    public string? PlantCity { get; set; }
    public string? Manufacturer { get; set; }
    public string? PlantCompanyName { get; set; }

    // Safety / Features
    public string? Abs { get; set; }
    public string? Esc { get; set; }
    public string? AirBagFront { get; set; }
    public string? AirBagSide { get; set; }
    public string? AirBagCurtain { get; set; }
    public string? LaneKeepSystem { get; set; }
    public string? BlindSpotMonitoring { get; set; }
    public string? Tpms { get; set; }
    public string? DaytimeRunningLights { get; set; }
    public string? KeylessIgnition { get; set; }
    public string? AdaptiveCruiseControl { get; set; }
    public string? LaneDepartureWarning { get; set; }
    public string? ParkAssist { get; set; }
    public string? AutomaticPedestrianAlertingSound { get; set; }
    public string? BlindSpotIntervention { get; set; }

    // Wheels / Dimensions
    public string? WheelBase { get; set; }
    public string? WheelSizeFront { get; set; }
    public string? WheelSizeRear { get; set; }
    public string? CurbWeight { get; set; }
    public string? TopSpeedMph { get; set; }

    // Errors / Status
    public string? ErrorCode { get; set; }
    public string? ErrorText { get; set; }

    // Metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
