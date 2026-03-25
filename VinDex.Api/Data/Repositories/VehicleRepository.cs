using Microsoft.EntityFrameworkCore;
using VinDex.Api.Data.Entities;

namespace VinDex.Api.Data.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly VinDexDbContext _context;

    public VehicleRepository(VinDexDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle?> GetByVinAsync(string vin)
    {
        var normalizedVin = vin.Trim().ToUpperInvariant();

        return await _context.Vehicles
            .FirstOrDefaultAsync(v => v.Vin == normalizedVin);
    }

    public async Task<IReadOnlyList<UserVehicle>> GetGarageEntriesByUserIdAsync(int userId)
    {
        return await _context.UserVehicles
            .AsNoTracking()
            .Include(uv => uv.Vehicle)
            .Where(uv => uv.UserId == userId)
            .OrderByDescending(uv => uv.SavedAt)
            .ToListAsync();
    }

    public async Task<UserVehicle?> GetGarageEntryAsync(int userId, int vehicleId)
    {
        return await _context.UserVehicles
            .FirstOrDefaultAsync(uv => uv.UserId == userId && uv.VehicleId == vehicleId);
    }

    public async Task AddAsync(Vehicle vehicle)
    {
        await _context.Vehicles.AddAsync(vehicle);
    }

    public async Task AddGarageEntryAsync(UserVehicle userVehicle)
    {
        await _context.UserVehicles.AddAsync(userVehicle);
    }

    public async Task RemoveGarageEntryAsync(UserVehicle userVehicle)
    {
        _context.UserVehicles.Remove(userVehicle);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}