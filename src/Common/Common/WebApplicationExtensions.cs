using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Common;

public static class WebApplicationExtensions
{
    public static WebApplication UseCommon(this WebApplication app)
    {
        if (app.Configuration.GetValue<bool?>("UseResponseCompression") ?? false)
        {
            app.UseResponseCompression();
        }

        app.MapControllers();

        return app;
    }
}
