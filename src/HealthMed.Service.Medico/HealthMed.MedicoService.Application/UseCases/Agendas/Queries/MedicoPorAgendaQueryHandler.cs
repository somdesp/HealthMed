using AutoMapper;
using HealthMed.BuildingBlocks.Contracts.Responses;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Queries
{
    public class MedicoPorAgendaQueryHandler : IRequestHandler<MedicoPorAgendaQuery, MeusAgendamentosResponse>
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IMedicoRepository _medicoRepository;

        public MedicoPorAgendaQueryHandler(
            IAgendaRepository agendaRepository,
            IMedicoRepository medicoRepository)
        {
            _agendaRepository = agendaRepository;
            _medicoRepository = medicoRepository;
        }

        public async Task<MeusAgendamentosResponse> Handle(MedicoPorAgendaQuery request, CancellationToken cancellationToken)
        {
            var agendas =
                await _agendaRepository.GetAsync(a => request.AgendasId.Contains(a.Id));

            var medicos = await _medicoRepository.GetAsync(m => agendas.Select(a => a.MedicoId).Contains(m.Id), null, "Especialidade");

            var response = new MeusAgendamentosResponse
            {
                Agendamentos = (from ag in agendas
                                join md in medicos on ag.MedicoId equals md.Id
                                select new AgendamentoResponse(ag.Id, ag.DataHora, md.Nome, md.Especialidade!.Nome, md.ValorConsulta)).ToList()
            };

            return response;
        }
    }
}