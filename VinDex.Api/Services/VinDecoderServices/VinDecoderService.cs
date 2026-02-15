using System.Globalization;
using System.Text.Json;
using VinDex.Api.Models.Nhtsa;
using VinDex.Api.Models.Vehicles;
using VinDex.Api.Data.Repositories;
using VinDex.Api.Services.Mappers;
using VinDex.Api.Utilities;

namespace VinDex.Api.Services;

public class VinDecoderService : IVinDecoderService
{
    private readonly HttpClient _httpClient;
    private readonly IVehicleRepository _repository;

    public VinDecoderService(HttpClient httpClient, IVehicleRepository repository)
    {
        _httpClient = httpClient;
        _repository = repository;
    }

    public async Task<VehicleInfoDto?> DecodeVinAsync(string vin)
    {
        var normalizedVin = VinUtils.Normalize(vin);

        var existing = await _repository.GetByVinAsync(normalizedVin);
        if (existing != null)
        {
            return VehicleMapper.ToDto(existing);
        }

        var url = $"https://vpic.nhtsa.dot.gov/api/vehicles/decodevinvalues/{normalizedVin}?format=json";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        var nhtsaResponse = JsonSerializer.Deserialize<NhtsaVinResponse>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

        var result = nhtsaResponse?.Results?.FirstOrDefault();
        if (result == null)
            return null;

        if (string.IsNullOrWhiteSpace(result.Vin) ||
            string.IsNullOrWhiteSpace(result.Make) ||
            string.IsNullOrWhiteSpace(result.Model))
        {
            return null;
        }


        if (!int.TryParse(result.ModelYear, NumberStyles.Integer, CultureInfo.InvariantCulture, out var modelYear))
            return null;

        var dto = new VehicleInfoDto
        {
            Vin = normalizedVin,
            Make = result.Make,
            Model = result.Model,
            Year = modelYear,
            Trim = result.Trim,
            Series = result.Series,
            BodyStyle = result.BodyClass,
            VehicleType = result.VehicleType,
            Doors = result.Doors,
            Engine = new EngineInfo
            {
                Cylinders = int.TryParse(result.EngineCylinders, NumberStyles.Integer, CultureInfo.InvariantCulture, out var cyl) ? cyl : null,
                Horsepower = int.TryParse(result.EngineHp, NumberStyles.Integer, CultureInfo.InvariantCulture, out var hp) ? hp : null,
                DisplacementLiters = double.TryParse(result.DisplacementLiters, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var dispLiters) ? dispLiters : null,
                FuelType = result.FuelTypePrimary,
                TransmissionStyle = result.TransmissionStyle,
                DriveType = result.DriveType,
                Axles = int.TryParse(result.Axles, NumberStyles.Integer, CultureInfo.InvariantCulture, out var axles) ? axles : null,
                Gvwr = result.Gvwr,
            },
            Manufacturing = new ManufacturingInfo
            {
                Country = result.PlantCountry,
                State = result.PlantState,
                City = result.PlantCity,
                Manufacturer = result.Manufacturer,
                PlantCompanyName = result.PlantCompanyName
            },
            // Safety & Restraints
            Abs = result.Abs,
            Esc = result.Esc,
            AirBagFront = result.AirBagLocFront,
            AirBagSide = result.AirBagLocSide,
            AirBagCurtain = result.AirBagLocCurtain,
            LaneKeepSystem = result.LaneKeepSystem,
            BlindSpotMonitoring = result.BlindSpotMon,
            Tpms = result.Tpms,
            DaytimeRunningLights = result.DaytimeRunningLight,

            // Wheels / Dimensions
            WheelBase = result.WheelBaseShort,
            WheelSizeFront = result.WheelSizeFront,
            WheelSizeRear = result.WheelSizeRear,
            CurbWeight = result.CurbWeightLb,
            TopSpeedMph = result.TopSpeedMph,

            // Options / Features
            KeylessIgnition = result.KeylessIgnition,
            AdaptiveCruiseControl = result.AdaptiveCruiseControl,
            LaneDepartureWarning = result.LaneDepartureWarning,
            ParkAssist = result.ParkAssist,
            AutomaticPedestrianAlertingSound = result.AutomaticPedestrianAlertingSound,
            BlindSpotIntervention = result.BlindSpotIntervention,

            // Errors / Status
            ErrorCode = result.ErrorCode,
            ErrorText = result.ErrorText
                };

        var entity = VehicleMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();

        return dto;
    }
}