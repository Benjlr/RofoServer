using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RofoServer.Persistence;

public class DesignTimeContext : IDesignTimeDbContextFactory<RofoDbContext>
{
    public RofoDbContext CreateDbContext(string[] args)
    {

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        DbContextOptionsBuilder<RofoDbContext> optionsBuilder = new DbContextOptionsBuilder<RofoDbContext>();
        optionsBuilder.UseLazyLoadingProxies().UseNpgsql(configuration.GetConnectionString("RofoDb"));

        return new RofoDbContext(optionsBuilder.Options);
    }
}