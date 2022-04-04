using Microsoft.AspNetCore.Builder;

namespace RofoServer.Utils.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
        => app
            .UseSwagger()
            .UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Rofo API");
                options.RoutePrefix = string.Empty;
            });
}