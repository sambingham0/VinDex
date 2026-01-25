using VinDex.Api.Data.Entities;

namespace VinDex.Api.Data.Repositories;

public interface IVehicleRepository
{
    Task<Vehicle?> GetByVinAsync(string vin);
    Task AddAsync(Vehicle vehicle);
    Task SaveChangesAsync();
}