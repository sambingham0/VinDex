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
        var nhtsaResponse = JsonSerializer.Deserialize<NhtsaVinResponse>(json);

        var result = nhtsaResponse?.Results?.FirstOrDefault();
        if (result == null)
            return null;

        var dto = new VehicleInfoDto
        {
            Vin = result.VIN,
            Make = result.Make,
            Model = result.Model,
            Year = int.TryParse(result.ModelYear, out var year) ? year : 0,
            BodyStyle = result.BodyClass,
            VehicleType = result.VehicleType,

            Engine = new EngineInfo
            {
                Cylinders = int.TryParse(result.EngineCylinders, out var cyl) ? cyl : null,
                Horsepower = int.TryParse(result.EngineHP, out var hp) ? hp : null,
                DisplacementLiters = double.TryParse(result.DisplacementL, out var disp) ? disp : null,
                FuelType = result.FuelTypePrimary
            },

            Manufacturing = new ManufacturingInfo
            {
                Country = result.PlantCountry,
                State = result.PlantState
            }
        };

        // Save to repository
        var entity = VehicleMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();

        return dto;
    }
}