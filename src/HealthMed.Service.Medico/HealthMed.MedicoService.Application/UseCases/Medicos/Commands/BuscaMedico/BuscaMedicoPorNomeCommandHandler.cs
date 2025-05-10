using AutoMapper;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.Dtos;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Medicos.Commands.BuscaMedico
{
    public partial class BuscaMedicoPorNomeCommandHandler : IRequestHandler<BuscaMedicoPorNomeCommantRequest, IEnumerable<MedicoDto>>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMapper _mapper;

        public BuscaMedicoPorNomeCommandHandler(IMedicoRepository medicoRepository, IMapper mapper)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedicoDto>> Handle(BuscaMedicoPorNomeCommantRequest request, CancellationToken cancellationToken)
        {
            var result = await _medicoRepository.BuscaMedico(request.NomeMedico);
            var medicoResponse = _mapper.Map<IEnumerable<MedicoDto>>(result);
            return medicoResponse;
        }

    }
}
