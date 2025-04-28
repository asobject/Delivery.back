using BuildingBlocks.Exceptions;
using Support.API.Hubs;

namespace Support.API.Services.App
{
    public static class ApplicationBuilderExtensionsService
    {
        public static void ConfigurePipeline(this WebApplication app)
        {
            app.UseCors("myCors");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.MapHub<ChatHub>("/chatHub");
            app.MapControllers();
        }
    }
}
