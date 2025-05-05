using AutoMapper;
using HealthMed.BuildingBlocks.Contracts;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.Dtos;
using HealthMed.MedicoService.Domain.Entities;
using MassTransit;

namespace HealthMed.MedicoService.Application.UseCases.Medicos.Events
{
    public partial class QueueBuscaMedicoConsumer : IConsumer<BuscaMedicoCommand>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMapper _mapper;

        public QueueBuscaMedicoConsumer(IMedicoRepository medicoRepository, IMapper mapper)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<BuscaMedicoCommand> context)
        {
            //Medico message = _mapper.Map<Medico>(context.Message);
            var result = await _medicoRepository.FirstOrDefaultAsync(x => x.Nome == context.Message.Nome);
            await context.RespondAsync(new MedicoResponse(result.Id, result.Nome, "!", result.Crm));

        }

    }
}
