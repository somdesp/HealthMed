using FluentValidation;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Domain.Entities.Enums;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.NovoAgendamento
{
    public class NovoAgendamentoValidator : AbstractValidator<NovoAgendamentoCommandRequest>
    {
        public NovoAgendamentoValidator(IAgendamentoRepository agendamentoRepository)
        {
            RuleFor(x => x.AgendaId)
                .MustAsync(async (agendaId, cancellation) =>
                {
                    var agendamentos = await agendamentoRepository.GetAsync(a => a.AgendaId == agendaId
                        && (a.Status != AgendamentoStatus.Cancelado || a.Status != AgendamentoStatus.Recusado));
                    return agendamentos == null;

                })
                .WithMessage("Agenda não esta disponível.");
        }
    }
}
