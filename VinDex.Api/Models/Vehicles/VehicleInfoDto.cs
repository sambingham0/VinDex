namespace VinDex.Api.Models.Vehicles;

public class VehicleInfoDto
{
    public string Vin { get; set; }

    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    public string BodyStyle { get; set; }
    public string VehicleType { get; set; }

    public EngineInfo Engine { get; set; }
    public ManufacturingInfo Manufacturing { get; set; }
}
