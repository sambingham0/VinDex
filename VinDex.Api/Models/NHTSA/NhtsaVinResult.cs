namespace VinDex.Api.Models.Nhtsa;

public class NhtsaVinResult
{
    public string VIN { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string ModelYear { get; set; }
    public string BodyClass { get; set; }
    public string VehicleType { get; set; }

    public string EngineCylinders { get; set; }
    public string EngineHP { get; set; }
    public string DisplacementL { get; set; }
    public string FuelTypePrimary { get; set; }

    public string TransmissionStyle { get; set; }
    public string DriveType { get; set; }

    public string PlantCountry { get; set; }
    public string PlantState { get; set; }

    public string GVWR { get; set; }
    public string ErrorText { get; set; }
}
