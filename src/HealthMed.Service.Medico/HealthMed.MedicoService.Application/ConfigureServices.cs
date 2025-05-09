using FluentValidation;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Configurations.Behaviors;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.AlteraAgenda;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.NovaAgenda;
using HealthMed.MedicoService.Application.UseCases.Medicos.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json.Serialization;

namespace HealthMed.MedicoService.Application;

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
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddScoped<IAppUsuario, AppUsuario>();
        services.AddScoped<IValidator<AlteraAgendaCommandRequest>, AlteraAgendaValidator>();
        services.AddScoped<IValidator<NovaAgendaCommandRequest>, NovaAgendaValidator>();



        return services;
    }
    public static IServiceCollection AddMassTransitExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(opt =>
        {
            opt.AddConsumer<QueueBuscaMedicoConsumer>();
            opt.AddConsumer<QueueBuscaMedicoEspecialidadeConsumer>();



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
