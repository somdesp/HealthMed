using AutoMapper;
using HealthMed.BuildingBlocks.Contracts.Events;
using HealthMed.BuildingBlocks.Contracts.Responses;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using MassTransit;

namespace HealthMed.MedicoService.Application.UseCases.Medicos.Events
{
    public partial class QueueBuscaMedicoEspecialidadeConsumer : IConsumer<BuscaEspecialidadeEvent>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMapper _mapper;

        public QueueBuscaMedicoEspecialidadeConsumer(IMedicoRepository medicoRepository, IMapper mapper)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<BuscaEspecialidadeEvent> context)
        {
            var result = await _medicoRepository.BuscaEspecialidade(context.Message.NomeEspecialidade);
            var medicoResponse = new BuscaMedicosResponse(_mapper.Map<IEnumerable<BuscaMedicoResponse>>(result));
            await context.RespondAsync(medicoResponse);
        }

    }
}
