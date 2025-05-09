using HealthMed.BuildingBlocks.Configurations;
using HealthMed.MedicoService.Application;
using HealthMed.MedicoService.Infrastructure;
using HealthMed.MedicoService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddSecurity(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
//builder.Services.AddMassTransitExtensionDefault(builder.Configuration);
builder.Services.AddMassTransitExtension(builder.Configuration);


builder.Services.AddControllers();
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

app.MapControllers();

app.UseHttpMetrics();

app.Run();