using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.DataBaseContext;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // WebApi loyihangizdagi appsettings fayllari joylashgan papka
        var projectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "AutoLife.WebApi");

        // Config qurilishi
        var configuration = new ConfigurationBuilder()
            .SetBasePath(projectPath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var connectionString = configuration.GetConnectionString("PostgresConnection");

        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}