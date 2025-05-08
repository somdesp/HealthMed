using FluentValidation;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.MedicoService.Application.Contracts.Persistence;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Commands.AlteraAgenda;

public class AlteraAgendaValidator : AbstractValidator<AlteraAgendaCommandRequest>
{
    public AlteraAgendaValidator(IAgendaRepository agendaRepository, IAppUsuario appUsuario)
    {
        RuleFor(x => x.DataHora)
            .Must(data => data > DateTime.Now)
            .WithMessage("A data/hora deve ser maior que a data/hora atual.");

        RuleFor(x => x.DataHora)
            .MustAsync(async (dataHora, cancellation) =>
            {
                var agendas = await agendaRepository.GetAsync(a => a.MedicoId == appUsuario.GetUsuarioId());
                return !agendas.Any(dataHoraExistente =>
                    dataHora == dataHoraExistente.DataHora ||
                    (dataHora > dataHoraExistente.DataHora.AddHours(-1) && dataHora < dataHoraExistente.DataHora.AddHours(1))
                );
            })
            .WithMessage("A data/hora já existe ou está em um intervalo de 1 hora de outra existente.");
    }
}