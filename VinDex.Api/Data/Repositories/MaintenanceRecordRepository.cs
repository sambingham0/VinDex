using Microsoft.EntityFrameworkCore;
using VinDex.Api.Data.Entities;

namespace VinDex.Api.Data.Repositories
{
    public class MaintenanceRecordRepository : IMaintenanceRecordRepository
    {
        private readonly VinDexDbContext _context;
        public MaintenanceRecordRepository(VinDexDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MaintenanceRecordEntity>> GetByVinAsync(string vin)
        {
            return await _context.MaintenanceRecords
                .Where(r => r.VehicleVin == vin)
                .OrderByDescending(r => r.Date)
                .ToListAsync();
        }

        public async Task<MaintenanceRecordEntity?> GetByIdAsync(int id)
        {
            return await _context.MaintenanceRecords.FindAsync(id);
        }

        public async Task<MaintenanceRecordEntity> AddAsync(MaintenanceRecordEntity record)
        {
            _context.MaintenanceRecords.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<MaintenanceRecordEntity> UpdateAsync(MaintenanceRecordEntity record)
        {
            _context.MaintenanceRecords.Update(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _context.MaintenanceRecords.FindAsync(id);
            if (record != null)
            {
                _context.MaintenanceRecords.Remove(record);
                await _context.SaveChangesAsync();
            }
        }
    }
}
