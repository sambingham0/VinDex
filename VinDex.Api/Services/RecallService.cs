using System.Globalization;
using System.Text.Json;
using VinDex.Api.Models.Nhtsa;
using VinDex.Api.Models.Recalls;

namespace VinDex.Api.Services;

public class RecallService
{
    private readonly HttpClient _httpClient;
    public RecallService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RecallDto>> DecodeRecallAsync(string make, string model, int year)
    {
        var encodedMake = Uri.EscapeDataString(make);
        var encodedModel = Uri.EscapeDataString(model);
        var url = $"https://api.nhtsa.gov/recalls/recallsByVehicle?make={encodedMake}&model={encodedModel}&modelYear={year}";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return new List<RecallDto>();

        var json = await response.Content.ReadAsStringAsync();
        var nhtsaResponse = JsonSerializer.Deserialize<NhtsaRecallResponse>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

        if (nhtsaResponse?.Results == null || nhtsaResponse.Results.Count == 0)
            return new List<RecallDto>();

        var dateFormats = new[] { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd" };

        var dtos = nhtsaResponse.Results
            .Select(r => new RecallDto
            {
                Manufacturer = r.Manufacturer,
                CampaignNumber = r.CampaignNumber,
                Component = r.Component,
                Summary = r.Summary,
                Consequence = r.Consequence,
                Remedy = r.Remedy,
                Notes = r.Notes,
                ReportReceivedDate = DateTime.TryParseExact(
                    r.ReportReceivedDateRaw,
                    dateFormats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var parsedDate)
                    ? parsedDate
                    : DateTime.TryParse(r.ReportReceivedDateRaw, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var parsedFallback)
                        ? parsedFallback
                        : null
            })
            .ToList();

        return dtos;
    }   
}