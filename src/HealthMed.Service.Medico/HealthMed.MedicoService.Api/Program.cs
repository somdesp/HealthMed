using HealthMed.BuildingBlocks.Configurations;
using HealthMed.MedicoService.Api.Consumers;
using HealthMed.MedicoService.Application;
using HealthMed.MedicoService.Infrastructure;
using HealthMed.MedicoService.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddSecurity(builder.Configuration);
builder.Services.AddMassTransit(opt =>
{
    opt.SetKebabCaseEndpointNameFormatter();

    opt.AddConsumer<AgendamentoCriadoConsumer>();

    opt.UsingRabbitMq(
        (context, cfg) =>
        {
            cfg.ConfigureJsonSerializerOptions(json =>
            {
                json.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                json.WriteIndented = true;
                return json;
            });

            cfg.Host(builder.Configuration.GetConnectionString("RabbitMq"));
            cfg.ServiceInstance(instance =>
            {
                instance.ConfigureJobServiceEndpoints();
                instance.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("fiap", false));
            });
        });

});


builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();
app.UseMetricServer();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MedicoContext>();
    db.Database.Migrate();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapMetrics();

app.MapControllers();

app.UseHttpMetrics();

app.Run();