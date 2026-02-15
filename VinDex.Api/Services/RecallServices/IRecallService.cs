using VinDex.Api.Models.Recalls;

namespace VinDex.Api.Services;

public interface IRecallService
{
    Task<List<RecallDto>> DecodeRecallAsync(string make, string model, int year);
}
