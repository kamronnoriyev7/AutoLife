using AutoLife.Identity.Configuration;
using AutoLife.Identity.Configurations;
using AutoLife.Identity.Models.IdentityEntities;
using Microsoft.EntityFrameworkCore;

namespace AutoLife.Identity;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }


    public DbSet<IdentityUser> IdentityUsers { get; set; } = default!;
    public DbSet<UserRole> UserRoles { get; set; } = default!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
    public DbSet<VerificationCode> VerificationCodes { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new IdentityUserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new VerificationCodeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
