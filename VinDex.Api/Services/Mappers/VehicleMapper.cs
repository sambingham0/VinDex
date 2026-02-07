using System.Globalization;
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
            Trim = vehicle.Trim,
            Series = vehicle.Series,
            BodyStyle = vehicle.BodyStyle,
            VehicleType = vehicle.VehicleType,
            Doors = vehicle.Doors,
            Engine = new EngineInfo
            {
                Cylinders = vehicle.EngineCylinders,
                Horsepower = vehicle.Horsepower,
                DisplacementLiters = vehicle.DisplacementLiters,
                FuelType = vehicle.FuelType,
                TransmissionStyle = vehicle.TransmissionStyle,
                DriveType = vehicle.DriveType,
                Axles = int.TryParse(vehicle.Axles, NumberStyles.Integer, CultureInfo.InvariantCulture, out var axles) ? axles : null,
                Gvwr = vehicle.Gvwr
            },
            Manufacturing = new ManufacturingInfo
            {
                Country = vehicle.PlantCountry,
                State = vehicle.PlantState,
                City = vehicle.PlantCity,
                Manufacturer = vehicle.Manufacturer,
                PlantCompanyName = vehicle.PlantCompanyName
            },
            Abs = vehicle.Abs,
            Esc = vehicle.Esc,
            AirBagFront = vehicle.AirBagFront,
            AirBagSide = vehicle.AirBagSide,
            AirBagCurtain = vehicle.AirBagCurtain,
            LaneKeepSystem = vehicle.LaneKeepSystem,
            BlindSpotMonitoring = vehicle.BlindSpotMonitoring,
            Tpms = vehicle.Tpms,
            DaytimeRunningLights = vehicle.DaytimeRunningLights,
            WheelBase = vehicle.WheelBase,
            WheelSizeFront = vehicle.WheelSizeFront,
            WheelSizeRear = vehicle.WheelSizeRear,
            CurbWeight = vehicle.CurbWeight,
            TopSpeedMph = vehicle.TopSpeedMph,
            KeylessIgnition = vehicle.KeylessIgnition,
            AdaptiveCruiseControl = vehicle.AdaptiveCruiseControl,
            LaneDepartureWarning = vehicle.LaneDepartureWarning,
            ParkAssist = vehicle.ParkAssist,
            AutomaticPedestrianAlertingSound = vehicle.AutomaticPedestrianAlertingSound,
            BlindSpotIntervention = vehicle.BlindSpotIntervention,
            ErrorCode = vehicle.ErrorCode,
            ErrorText = vehicle.ErrorText
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
            Trim = dto.Trim,
            Series = dto.Series,
            BodyStyle = dto.BodyStyle,
            VehicleType = dto.VehicleType,
            Doors = dto.Doors,
            EngineCylinders = dto.Engine?.Cylinders,
            Horsepower = dto.Engine?.Horsepower,
            DisplacementLiters = dto.Engine?.DisplacementLiters,
            FuelType = dto.Engine?.FuelType,
            TransmissionStyle = dto.Engine?.TransmissionStyle,
            DriveType = dto.Engine?.DriveType,
            Axles = dto.Engine?.Axles?.ToString(CultureInfo.InvariantCulture),
            Gvwr = dto.Engine?.Gvwr,
            PlantCountry = dto.Manufacturing?.Country,
            PlantState = dto.Manufacturing?.State,
            PlantCity = dto.Manufacturing?.City,
            Manufacturer = dto.Manufacturing?.Manufacturer,
            PlantCompanyName = dto.Manufacturing?.PlantCompanyName,
            Abs = dto.Abs,
            Esc = dto.Esc,
            AirBagFront = dto.AirBagFront,
            AirBagSide = dto.AirBagSide,
            AirBagCurtain = dto.AirBagCurtain,
            LaneKeepSystem = dto.LaneKeepSystem,
            BlindSpotMonitoring = dto.BlindSpotMonitoring,
            Tpms = dto.Tpms,
            DaytimeRunningLights = dto.DaytimeRunningLights,
            WheelBase = dto.WheelBase,
            WheelSizeFront = dto.WheelSizeFront,
            WheelSizeRear = dto.WheelSizeRear,
            CurbWeight = dto.CurbWeight,
            TopSpeedMph = dto.TopSpeedMph,
            KeylessIgnition = dto.KeylessIgnition,
            AdaptiveCruiseControl = dto.AdaptiveCruiseControl,
            LaneDepartureWarning = dto.LaneDepartureWarning,
            ParkAssist = dto.ParkAssist,
            AutomaticPedestrianAlertingSound = dto.AutomaticPedestrianAlertingSound,
            BlindSpotIntervention = dto.BlindSpotIntervention,
            ErrorCode = dto.ErrorCode,
            ErrorText = dto.ErrorText,
            CreatedAt = DateTime.UtcNow
        };
    }
}