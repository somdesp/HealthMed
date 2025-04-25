using HealthMed.MedicoService.Application;
using HealthMed.MedicoService.Application.Settings;
using HealthMed.MedicoService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSecurity(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
