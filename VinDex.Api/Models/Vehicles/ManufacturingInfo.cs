namespace VinDex.Api.Models.Vehicles;

public class ManufacturingInfo
{
    // Country, state, and city of the plant
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }

    // Company/manufacturer names
    public string? Manufacturer { get; set; }
    public string? PlantCompanyName { get; set; }
}
