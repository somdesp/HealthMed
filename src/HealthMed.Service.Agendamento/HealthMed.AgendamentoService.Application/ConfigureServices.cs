using FluentValidation;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Configurations.Behaviors;
using MassTransit;
using MediatR;
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

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddScoped<IAppUsuario, AppUsuario>();

        return services;
    }
}
