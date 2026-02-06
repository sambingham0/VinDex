using System.Text.Json.Serialization;

namespace VinDex.Api.Models.Nhtsa;

public class NhtsaRecallResult
{
    [JsonPropertyName("Manufacturer")]
    public string? Manufacturer { get; set; }

    [JsonPropertyName("NHTSACampaignNumber")]
    public string? CampaignNumber { get; set; }

    [JsonPropertyName("Component")]
    public string? Component { get; set; }

    [JsonPropertyName("Summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("Consequence")]
    public string? Consequence { get; set; }

    [JsonPropertyName("Remedy")]
    public string? Remedy { get; set; }

    [JsonPropertyName("Notes")]
    public string? Notes { get; set; }

    [JsonPropertyName("ReportReceivedDate")]
    public string? ReportReceivedDateRaw { get; set; }
}
