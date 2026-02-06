namespace VinDex.Api.Models.Nhtsa;

public class NhtsaRecallResponse
{
    public int Count { get; set; }
    public string? Message { get; set; }
    public List<NhtsaRecallResult> Results { get; set; } = new();
}
