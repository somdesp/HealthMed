using AutoMapper;
using HealthMed.BuildingBlocks.Contracts.Events;
using HealthMed.BuildingBlocks.Contracts.Responses;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using MassTransit;

namespace HealthMed.MedicoService.Application.UseCases.Medicos.Events
{
    public partial class QueueBuscaMedicoConsumer : IConsumer<BuscaMedicoEvent>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMapper _mapper;

        public QueueBuscaMedicoConsumer(IMedicoRepository medicoRepository, IMapper mapper)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<BuscaMedicoEvent> context)
        {
            var result = await _medicoRepository.BuscaMedico(context.Message.Nome);
            var medicoResponse = new BuscaMedicosResponse(_mapper.Map<IEnumerable<BuscaMedicoResponse>>(result));
            await context.RespondAsync(medicoResponse);
        }

    }
}
