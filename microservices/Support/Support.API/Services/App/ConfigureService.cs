
using Application.Features.Commands.SendMessage;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Interfaces.Services;
using Domain.Interfaces;
using Domain.Interfaces.Hubs;
using Domain.Interfaces.Notifications;
using Infrastructure.Notifications;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.SignalR;
using Support.API.Extensions;
using Support.API.Hubs;

namespace Support.API.Services.App
{
    public static class ConfigureService
    {
        public static void ConfigureHosting(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSignalR();
            services.AddHttpContextAccessor();

            services.ConfigureCors(configuration);
            services.ConfigureMongo(configuration);
            services.AddMediatR(cfg =>
         cfg.RegisterServicesFromAssembly(typeof(SendMessageCommand).Assembly));
            services.AddScoped<IChatNotifier, SignalRChatNotifier>();
            services.AddScoped<IChatHubContext, SignalRChatHubContext>();
            services.AddScoped<IChatRepository, MongoChatRepository>();

            services.AddScoped<IAppConfiguration, AppConfiguration>();


            services.AddControllers();

            services.AddLogging();

            services.AddTransient<GlobalExceptionMiddleware>();

            services.Configure<RouteOptions>(options => {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

      
        }
    }
}
