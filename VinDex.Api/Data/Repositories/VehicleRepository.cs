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
        return await _context.Vehicles
            .FirstOrDefaultAsync(v => v.Vin == vin);
    }

    public async Task AddAsync(Vehicle vehicle)
    {
        await _context.Vehicles.AddAsync(vehicle);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}