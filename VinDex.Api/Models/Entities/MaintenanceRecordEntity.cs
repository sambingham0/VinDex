using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinDex.Api.Data.Entities
{
    [Table("MaintenanceRecords")]
    public class MaintenanceRecordEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string VehicleVin { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = string.Empty;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Mileage { get; set; }
        public string? Notes { get; set; }
    }
}
