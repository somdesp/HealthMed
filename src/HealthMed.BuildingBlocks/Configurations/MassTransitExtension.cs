using HealthMed.BuildingBlocks.Contracts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMed.BuildingBlocks.Configurations
{
    public static class MassTransitExtension
    {
        public static void AddMassTransitExtensionDefault(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMassTransit(opt =>
            {
                opt.SetKebabCaseEndpointNameFormatter();


                opt.UsingRabbitMq(
                    (context, cfg) =>
                    {
                        cfg.Host(configuration.GetConnectionString("RabbitMq"));
                        cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("fiap", false));
                        cfg.UseMessageRetry(retry => { retry.Interval(3, TimeSpan.FromSeconds(5)); });
                    });
            });
        }
    }
}
