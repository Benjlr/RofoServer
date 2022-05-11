using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Persistence
{
    public class RofoDbContext : DbContext
    {
        public  DbSet<Rofo> Rofos { get; set; }
        public  DbSet<RofoUser> Users { get; set; }
        public  DbSet<RofoGroup> Groups { get; set; }
        public  DbSet<RofoGroupAccess> GroupAccess { get; set; }

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
}
