using Microsoft.EntityFrameworkCore;
using VinDex.Api.Data.Entities;

namespace VinDex.Api.Data;

public class VinDexDbContext : DbContext
{
    public VinDexDbContext(DbContextOptions<VinDexDbContext> options) : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserVehicle> UserVehicles => Set<UserVehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.GoogleSubjectId)
            .IsUnique();

        modelBuilder.Entity<Vehicle>()
            .HasIndex(v => v.Vin)
            .IsUnique();

        modelBuilder.Entity<UserVehicle>()
            .HasKey(uv => new { uv.UserId, uv.VehicleId });

        modelBuilder.Entity<UserVehicle>()
            .HasOne(uv => uv.User)
            .WithMany(u => u.SavedVehicles)
            .HasForeignKey(uv => uv.UserId);

        modelBuilder.Entity<UserVehicle>()
            .HasOne(uv => uv.Vehicle)
            .WithMany(v => v.SavedByUsers)
            .HasForeignKey(uv => uv.VehicleId);

        modelBuilder.Entity<UserVehicle>()
            .HasIndex(uv => uv.VehicleId);
    }
}