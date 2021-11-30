using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;
using System;

namespace RofoServer.Persistence
{
    public class RofoDbContext : DbContext
    {
        public  DbSet<Rofo> Rofos { get; set; }
        public  DbSet<User> Users { get; set; }

        public RofoDbContext(DbContextOptions<RofoDbContext> options)
            : base(options) {
        }

        public RofoDbContext()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    IConfigurationRoot configuration = new ConfigurationBuilder()
        //        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    optionsBuilder.UseLazyLoadingProxies().UseNpgsql(configuration.GetConnectionString("RofoDb"));
        //}
    }

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
}
