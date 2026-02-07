using System.Text.Json.Serialization;

namespace VinDex.Api.Models.Nhtsa;

public class NhtsaVinResult
{
    // Basic Vehicle Identity
    [JsonPropertyName("VIN")]
    public string? Vin { get; set; }

    [JsonPropertyName("Make")]
    public string? Make { get; set; }

    [JsonPropertyName("Model")]
    public string? Model { get; set; }

    [JsonPropertyName("ModelYear")]
    public string? ModelYear { get; set; }

    [JsonPropertyName("Trim")]
    public string? Trim { get; set; }

    [JsonPropertyName("BodyClass")]
    public string? BodyClass { get; set; }

    [JsonPropertyName("VehicleType")]
    public string? VehicleType { get; set; }

    [JsonPropertyName("Series")]
    public string? Series { get; set; }

    // Manufacturer Info
    [JsonPropertyName("Manufacturer")]
    public string? Manufacturer { get; set; }

    [JsonPropertyName("PlantCompanyName")]
    public string? PlantCompanyName { get; set; }

    [JsonPropertyName("PlantCountry")]
    public string? PlantCountry { get; set; }

    [JsonPropertyName("PlantState")]
    public string? PlantState { get; set; }

    [JsonPropertyName("PlantCity")]
    public string? PlantCity { get; set; }

    // Engine / Drivetrain
    [JsonPropertyName("EngineCylinders")]
    public string? EngineCylinders { get; set; }

    [JsonPropertyName("EngineHP")]
    public string? EngineHp { get; set; }

    [JsonPropertyName("EngineKW")]
    public string? EngineKw { get; set; }

    [JsonPropertyName("DisplacementL")]
    public string? DisplacementLiters { get; set; }

    [JsonPropertyName("FuelTypePrimary")]
    public string? FuelTypePrimary { get; set; }

    [JsonPropertyName("FuelTypeSecondary")]
    public string? FuelTypeSecondary { get; set; }

    [JsonPropertyName("TransmissionStyle")]
    public string? TransmissionStyle { get; set; }

    [JsonPropertyName("DriveType")]
    public string? DriveType { get; set; }

    [JsonPropertyName("Axles")]
    public string? Axles { get; set; }

    [JsonPropertyName("GVWR")]
    public string? Gvwr { get; set; }

    // Safety & Restraints
    [JsonPropertyName("ABS")]
    public string? Abs { get; set; }

    [JsonPropertyName("ESC")]
    public string? Esc { get; set; }

    [JsonPropertyName("AirBagLocFront")]
    public string? AirBagLocFront { get; set; }

    [JsonPropertyName("AirBagLocSide")]
    public string? AirBagLocSide { get; set; }

    [JsonPropertyName("AirBagLocCurtain")]
    public string? AirBagLocCurtain { get; set; }

    [JsonPropertyName("LaneKeepSystem")]
    public string? LaneKeepSystem { get; set; }

    [JsonPropertyName("BlindSpotMon")]
    public string? BlindSpotMon { get; set; }

    [JsonPropertyName("TPMS")]
    public string? Tpms { get; set; }

    [JsonPropertyName("DaytimeRunningLight")]
    public string? DaytimeRunningLight { get; set; }

    // Wheels / Dimensions
    [JsonPropertyName("Doors")]
    public string? Doors { get; set; }

    [JsonPropertyName("WheelBaseShort")]
    public string? WheelBaseShort { get; set; }

    [JsonPropertyName("WheelSizeFront")]
    public string? WheelSizeFront { get; set; }

    [JsonPropertyName("WheelSizeRear")]
    public string? WheelSizeRear { get; set; }

    [JsonPropertyName("CurbWeightLB")]
    public string? CurbWeightLb { get; set; }

    [JsonPropertyName("TopSpeedMPH")]
    public string? TopSpeedMph { get; set; }

    // Options / Features
    [JsonPropertyName("KeylessIgnition")]
    public string? KeylessIgnition { get; set; }

    [JsonPropertyName("AdaptiveCruiseControl")]
    public string? AdaptiveCruiseControl { get; set; }

    [JsonPropertyName("LaneDepartureWarning")]
    public string? LaneDepartureWarning { get; set; }

    [JsonPropertyName("ParkAssist")]
    public string? ParkAssist { get; set; }

    [JsonPropertyName("AutomaticPedestrianAlertingSound")]
    public string? AutomaticPedestrianAlertingSound { get; set; }

    [JsonPropertyName("BlindSpotIntervention")]
    public string? BlindSpotIntervention { get; set; }

    // Errors / Status
    [JsonPropertyName("ErrorCode")]
    public string? ErrorCode { get; set; }

    [JsonPropertyName("ErrorText")]
    public string? ErrorText { get; set; }
}