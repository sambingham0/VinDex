using Microsoft.EntityFrameworkCore;
using VinDex.Api.Data.Entities;

namespace VinDex.Api.Data;

public class VinDexDbContext : DbContext
{
    public VinDexDbContext(DbContextOptions<VinDexDbContext> options) : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>()
            .HasIndex(v => v.Vin)
            .IsUnique();
    }
}