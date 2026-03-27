namespace VinDex.Api.Models.Vehicles
{
    public class MaintenanceRecord
    {
        public int Id { get; set; }
        public string VehicleVin { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Mileage { get; set; }
        public string? Notes { get; set; }
    }
}
