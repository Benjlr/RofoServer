using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RofoServer.Persistence;
using System.Text;

namespace RofoServer.Utils.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration config)
        => services
            .AddDbContext<RofoDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("RofoDb")));



        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config) {
            services.AddAuthentication(x => {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = 
                            new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(config["Rofos:ApiKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services;

        public static IServiceCollection AddSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Rofo API",
                        Version = "v1"
                    });
            });

        public static void AddApiControllers(this IServiceCollection services)
            => services
                .AddControllers(options => options
                    .Filters
                    .Add<ModelOrNotFoundActionFilter>());
    }
}
