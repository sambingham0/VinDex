using VinDex.Api.Data.Entities;

namespace VinDex.Api.Data.Repositories
{
    public interface IMaintenanceRecordRepository
    {
        Task<IEnumerable<MaintenanceRecordEntity>> GetByVinAsync(string vin);
        Task<MaintenanceRecordEntity?> GetByIdAsync(int id);
        Task<MaintenanceRecordEntity> AddAsync(MaintenanceRecordEntity record);
        Task<MaintenanceRecordEntity> UpdateAsync(MaintenanceRecordEntity record);
        Task DeleteAsync(int id);
    }
}
