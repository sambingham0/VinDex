namespace VinDex.Api.Models.Recalls;

public class RecallDto
{
    public string? Manufacturer { get; set; }
    public string? CampaignNumber { get; set; }
    public string? Component { get; set; }
    public string? Summary { get; set; }
    public string? Consequence { get; set; }
    public string? Remedy { get; set; }
    public string? Notes { get; set; }
    public DateTime? ReportReceivedDate { get; set; }
}
