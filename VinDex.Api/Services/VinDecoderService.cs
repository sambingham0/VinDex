using System.Globalization;
using System.Text.Json;
using VinDex.Api.Models.Nhtsa;
using VinDex.Api.Models.Vehicles;
using VinDex.Api.Data.Repositories;
using VinDex.Api.Services.Mappers;

namespace VinDex.Api.Services;

public class VinDecoderService
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
        var existing = await _repository.GetByVinAsync(vin);
        if (existing != null)
        {
            return VehicleMapper.ToDto(existing);
        }

        var url = $"https://vpic.nhtsa.dot.gov/api/vehicles/decodevinvalues/{vin}?format=json";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        var nhtsaResponse = JsonSerializer.Deserialize<NhtsaVinResponse>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

        var result = nhtsaResponse?.Results?.FirstOrDefault();
        if (result == null)
            return null;

        if (!int.TryParse(result.ModelYear, NumberStyles.Integer, CultureInfo.InvariantCulture, out var modelYear))
            return null;

        var dto = new VehicleInfoDto
        {
            Vin = result.Vin,
            Make = result.Make,
            Model = result.Model,
            Year = modelYear,
            BodyStyle = result.BodyClass,
            VehicleType = result.VehicleType,

            Engine = new EngineInfo
            {
                Cylinders = int.TryParse(result.EngineCylinders, NumberStyles.Integer, CultureInfo.InvariantCulture, out var cyl) ? cyl : null,
                Horsepower = int.TryParse(result.EngineHp, NumberStyles.Integer, CultureInfo.InvariantCulture, out var hp) ? hp : null,
                DisplacementLiters = double.TryParse(result.DisplacementLiters, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var disp) ? disp : null,
                FuelType = result.FuelTypePrimary
            },

            Manufacturing = new ManufacturingInfo
            {
                Country = result.PlantCountry,
                State = result.PlantState
            }
        };

        var entity = VehicleMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();

        return dto;
    }
}