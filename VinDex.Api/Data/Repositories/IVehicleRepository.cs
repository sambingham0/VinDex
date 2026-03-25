using VinDex.Api.Data.Entities;

namespace VinDex.Api.Data.Repositories;

public interface IVehicleRepository
{
    Task<Vehicle?> GetByVinAsync(string vin);
    Task<IReadOnlyList<UserVehicle>> GetGarageEntriesByUserIdAsync(int userId);
    Task<UserVehicle?> GetGarageEntryAsync(int userId, int vehicleId);
    Task AddAsync(Vehicle vehicle);
    Task AddGarageEntryAsync(UserVehicle userVehicle);
    Task RemoveGarageEntryAsync(UserVehicle userVehicle);
    Task SaveChangesAsync();
}