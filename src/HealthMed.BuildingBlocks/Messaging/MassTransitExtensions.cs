using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMed.BuildingBlocks.Messaging;

public static class MassTransitExtensions
{
    public static void AddDefaultMassTransit(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("rabbitmq", "/", h => {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}
