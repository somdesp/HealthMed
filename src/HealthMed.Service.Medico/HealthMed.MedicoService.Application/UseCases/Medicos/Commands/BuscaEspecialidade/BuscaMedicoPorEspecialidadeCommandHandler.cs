using AutoMapper;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.Dtos;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Medicos.Commands.BuscaEspecialidade
{
    public partial class BuscaMedicoPorEspecialidadeCommandHandler : IRequestHandler<BuscaMedicoPorEspecialidadeCommandRequest, IEnumerable<MedicoDto>>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMapper _mapper;

        public BuscaMedicoPorEspecialidadeCommandHandler(IMedicoRepository medicoRepository, IMapper mapper)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedicoDto>> Handle(BuscaMedicoPorEspecialidadeCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _medicoRepository.BuscaEspecialidade(request.NomeEspecialidade);
            var medicoResponse = _mapper.Map<IEnumerable<MedicoDto>>(result);
            return medicoResponse;
        }

    }
}
