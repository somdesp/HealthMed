using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json.Serialization;

namespace HealthMed.AgendamentoService.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

        return services;
    }
    public static IServiceCollection AddMassTransitExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(opt =>
        {
            opt.SetKebabCaseEndpointNameFormatter();

            opt.UsingRabbitMq(
                (context, cfg) =>
                {
                    cfg.ConfigureJsonSerializerOptions(json =>
                    {
                        json.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                        json.WriteIndented = true;
                        return json;
                    });

                    cfg.Host(configuration.GetConnectionString("RabbitMq"));
                    cfg.ServiceInstance(instance =>
                    {
                        instance.ConfigureJobServiceEndpoints();
                        instance.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("fiap", false));
                    });
                });

        });

        return services;
    }
}
