using System.Text.Json.Serialization;

namespace VinDex.Api.Models.Nhtsa;

public class NhtsaVinResult
{
    [JsonPropertyName("VIN")]
    public string Vin { get; set; }

    [JsonPropertyName("Make")]
    public string Make { get; set; }

    [JsonPropertyName("Model")]
    public string Model { get; set; }

    [JsonPropertyName("ModelYear")]
    public string ModelYear { get; set; }

    [JsonPropertyName("BodyClass")]
    public string BodyClass { get; set; }

    [JsonPropertyName("VehicleType")]
    public string VehicleType { get; set; }

    [JsonPropertyName("EngineCylinders")]
    public string EngineCylinders { get; set; }

    [JsonPropertyName("EngineHP")]
    public string EngineHp { get; set; }

    [JsonPropertyName("DisplacementL")]
    public string DisplacementLiters { get; set; }

    [JsonPropertyName("FuelTypePrimary")]
    public string FuelTypePrimary { get; set; }

    [JsonPropertyName("TransmissionStyle")]
    public string TransmissionStyle { get; set; }

    [JsonPropertyName("DriveType")]
    public string DriveType { get; set; }

    [JsonPropertyName("PlantCountry")]
    public string PlantCountry { get; set; }

    [JsonPropertyName("PlantState")]
    public string PlantState { get; set; }

    [JsonPropertyName("GVWR")]
    public string Gvwr { get; set; }

    [JsonPropertyName("ErrorCode")]
    public string ErrorCode { get; set; }

    [JsonPropertyName("ErrorText")]
    public string ErrorText { get; set; }
}
