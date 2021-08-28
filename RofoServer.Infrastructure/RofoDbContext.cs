using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RofoServer.Domain;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Persistence
{
    public class RofoDbContext : DbContext, IRofoREpository
    {
        public  DbSet<Rofo> Rofos { get; set; }
        public  DbSet<User> Users { get; set; }
        public  DbSet<UserClaims> UserClaims { get; set; }
        public  DbSet<Claims> Claims { get; set; }

        public RofoDbContext(DbContextOptions<RofoDbContext> options)
            : base(options) {
        }

        public RofoDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(configuration.GetConnectionString("RofoDb"));
        }

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task AddRofo(Rofo rofo)
        {
            throw new NotImplementedException();
        }

        public Task<Rofo> GetRofo(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class DesignTimeContext : IDesignTimeDbContextFactory<RofoDbContext>
    {
        public RofoDbContext CreateDbContext(string[] args) {

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
