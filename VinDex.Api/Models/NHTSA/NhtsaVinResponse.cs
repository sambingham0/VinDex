namespace VinDex.Api.Models.Nhtsa;

public class NhtsaVinResponse
{
    public int Count { get; set; }
    public string? Message { get; set; }
    public string? SearchCriteria { get; set; }
    public List<NhtsaVinResult> Results { get; set; } = [];
}
