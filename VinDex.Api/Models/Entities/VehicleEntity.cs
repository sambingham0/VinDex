namespace VinDex.Api.Data.Entities;

public class Vehicle
{
    public int Id { get; set; }

    public string Vin { get; set; } = null!;

    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    public string BodyStyle { get; set; }
    public string VehicleType { get; set; }

    public int? EngineCylinders { get; set; }
    public int? Horsepower { get; set; }
    public double? DisplacementLiters { get; set; }
    public string FuelType { get; set; }

    public string PlantCountry { get; set; }
    public string PlantState { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}