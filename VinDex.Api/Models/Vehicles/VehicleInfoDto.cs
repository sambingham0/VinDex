namespace VinDex.Api.Models.Vehicles;

public class VehicleInfoDto
{
    // Basic Vehicle Identity
    public required string Vin { get; set; }
    public required string Make { get; set; }
    public required string Model { get; set; }
    public required int Year { get; set; }

    public string? Trim { get; set; }
    public string? Series { get; set; }
    public string? BodyStyle { get; set; }
    public string? VehicleType { get; set; }
    public string? Doors { get; set; }

    // Engine & Manufacturing
    public EngineInfo? Engine { get; set; }
    public ManufacturingInfo? Manufacturing { get; set; }

    // Safety & Restraints
    public string? Abs { get; set; }
    public string? Esc { get; set; }
    public string? AirBagFront { get; set; }
    public string? AirBagSide { get; set; }
    public string? AirBagCurtain { get; set; }
    public string? LaneKeepSystem { get; set; }
    public string? BlindSpotMonitoring { get; set; }
    public string? Tpms { get; set; }
    public string? DaytimeRunningLights { get; set; }

    // Wheels / Dimensions
    public string? WheelBase { get; set; }
    public string? WheelSizeFront { get; set; }
    public string? WheelSizeRear { get; set; }
    public string? CurbWeight { get; set; }
    public string? TopSpeedMph { get; set; }

    // Options / Features
    public string? KeylessIgnition { get; set; }
    public string? AdaptiveCruiseControl { get; set; }
    public string? LaneDepartureWarning { get; set; }
    public string? ParkAssist { get; set; }
    public string? AutomaticPedestrianAlertingSound { get; set; }
    public string? BlindSpotIntervention { get; set; }

    // Errors / Status
    public string? ErrorCode { get; set; }
    public string? ErrorText { get; set; }
}
