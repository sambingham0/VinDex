namespace VinDex.Api.Data.Entities;

public class UserVehicle
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;
}