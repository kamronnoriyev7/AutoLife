using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AutoLife.Identity;

public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(string[] args)
    {
        // WebApi loyihangizdagi appsettings fayllari joylashgan papka
        var projectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "AutoLife.WebApi");

        // Config qurilishi
        var configuration = new ConfigurationBuilder()
            .SetBasePath(projectPath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();

        var connectionString = configuration.GetConnectionString("MSSQLConnection");

        optionsBuilder.UseSqlServer(connectionString);

        return new IdentityDbContext(optionsBuilder.Options);
    }
}