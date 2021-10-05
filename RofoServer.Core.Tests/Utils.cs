using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;
using RofoServer.Persistence;

namespace RofoServer.Core.Tests
{
    public class Utils
    {
        public static IConfiguration TestConfiguration()
        {
            var myConfiguration = new Dictionary<string, string> {
                { "Rofos:ApiKey", "unit testing key for testing purpose"},
                {"RefreshTokenExpiryDays", "2"},
                {"JWTExpiryMinutes", "5"},
                {"ServerIPAddress", "RofoServer"},
                {"HostPort", "5000"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            return configuration;
        }

        public static RofoDbContext TestContext() {
            var options = new DbContextOptionsBuilder<RofoDbContext>()
                .UseInMemoryDatabase(databaseName: "RofoDatabase")
                ;

            return new RofoDbContext(options.Options);
        }
    }
}
