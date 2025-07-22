using AutoLife.Domain.Entities;
using AutoLife.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.DataBaseContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Parking> Parkings { get; set; } = default!;
    public DbSet<Booking> Bookings { get; set; } = default!; 
    public DbSet<Address> Addresses { get; set; } = default!;
    public DbSet<FuelPrice> FuelPrices { get; set; } = default!;
    public DbSet<Rating> Ratings { get; set; } = default!;
    public DbSet<Image> Photos { get; set; } = default!;
    public DbSet<ParkingPrice> ParkingPrices { get; set; } = default!;
    public DbSet<AppFeedback> AppFeedbacks { get; set; } = default!;
    public DbSet<Favorite> Favorites { get; set; } = default!;
    public DbSet<FuelStation> FuelStations { get; set; } = default!;
    public DbSet<GeoLocation> GeoLocations { get; set; } = default!;
    public DbSet<Image> Images { get; set; } = default!;
    public DbSet<News> News { get; set; } = default!;
    public DbSet<Notification> Notifications { get; set; } = default!;
    public DbSet<ServiceCenter> ServiceCenters { get; set; } = default!;
    public DbSet<Vehicle> Vehicles { get; set; } = default!;
    public DbSet<Country> Countries { get; set; } = default!;
    public DbSet<District> Districts { get; set; } = default!;
    public DbSet<Region> Regions { get; set; } = default!;
    public DbSet<Company> Companies { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

