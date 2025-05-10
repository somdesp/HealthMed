using HealthMed.MedicoService.Application.Exceptions;
using HealthMed.MedicoService.Application.UseCases.Medicos.Commands.BuscaEspecialidade;
using HealthMed.MedicoService.Application.UseCases.Medicos.Commands.BuscaMedico;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.MedicoService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("BuscaMedico")]
        public async Task<ActionResult> BuscaMedico([FromBody] BuscaMedicoPorNomeCommantRequest query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpPost("BuscaEspecialidade")]
        public async Task<ActionResult> BuscaEspecialidade([FromBody] BuscaMedicoPorEspecialidadeCommandRequest query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }
    }
}
