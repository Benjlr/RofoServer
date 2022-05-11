using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RofoServer.Utils.Extensions;

namespace RofoServer;

public class Startup
{
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
        => services
            .AddDatabases(this.Configuration)
            .AddJwtAuthentication(this.Configuration)
            .AddApplicationServices()
            .AddSwagger()
            .AddApiControllers();

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app
            
            .UseSwaggerUI()
            .UseRouting()
            .UseCors(options => options
                .WithOrigins("https://rofos.azurewebsites.net/").AllowAnyMethod().AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod())
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints =>
                endpoints.MapControllers());
    }
}