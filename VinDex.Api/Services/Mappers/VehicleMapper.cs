using VinDex.Api.Data.Entities;
using VinDex.Api.Models.Vehicles;

namespace VinDex.Api.Services.Mappers;

public static class VehicleMapper
{
    public static VehicleInfoDto ToDto(Vehicle vehicle)
    {
        return new VehicleInfoDto
        {
            Vin = vehicle.Vin,
            Make = vehicle.Make,
            Model = vehicle.Model,
            Year = vehicle.Year,
            BodyStyle = vehicle.BodyStyle,
            VehicleType = vehicle.VehicleType,
            Engine = new EngineInfo
            {
                Cylinders = vehicle.EngineCylinders,
                Horsepower = vehicle.Horsepower,
                DisplacementLiters = vehicle.DisplacementLiters,
                FuelType = vehicle.FuelType
            },
            Manufacturing = new ManufacturingInfo
            {
                Country = vehicle.PlantCountry,
                State = vehicle.PlantState
            }
        };
    }

    public static Vehicle ToEntity(VehicleInfoDto dto)
    {
        return new Vehicle
        {
            Vin = dto.Vin,
            Make = dto.Make,
            Model = dto.Model,
            Year = dto.Year,
            BodyStyle = dto.BodyStyle,
            VehicleType = dto.VehicleType,
            EngineCylinders = dto.Engine.Cylinders,
            Horsepower = dto.Engine.Horsepower,
            DisplacementLiters = dto.Engine.DisplacementLiters,
            FuelType = dto.Engine.FuelType,
            PlantCountry = dto.Manufacturing.Country,
            PlantState = dto.Manufacturing.State,
            CreatedAt = DateTime.UtcNow
        };
    }
}