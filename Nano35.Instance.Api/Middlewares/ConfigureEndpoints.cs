using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Nano35.Instance.Api.Middlewares
{
    public class ConfigureEndpoints
    {
        public static void Configure(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    context.Response.Redirect("/swagger");
                });
            });
        }
    }
}