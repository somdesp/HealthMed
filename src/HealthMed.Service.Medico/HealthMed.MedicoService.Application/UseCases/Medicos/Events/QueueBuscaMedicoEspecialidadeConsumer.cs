using AutoMapper;
using HealthMed.BuildingBlocks.Contracts;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using MassTransit;

namespace HealthMed.MedicoService.Application.UseCases.Medicos.Events
{
    public partial class QueueBuscaMedicoEspecialidadeConsumer : IConsumer<BuscaMedicoEspecialidadeCommand>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMapper _mapper;

        public QueueBuscaMedicoEspecialidadeConsumer(IMedicoRepository medicoRepository, IMapper mapper)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<BuscaMedicoEspecialidadeCommand> context)
        {
            var result = await _medicoRepository.BuscaEspecialidade(context.Message.Nome);
            var medicoResponse = new MedicosResponse(_mapper.Map<IEnumerable<MedicoResponse>>(result));
            await context.RespondAsync(medicoResponse);
        }

    }
}
