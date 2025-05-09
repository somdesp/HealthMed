using HealthMed.BuildingBlocks.Contracts.Events;
using HealthMed.BuildingBlocks.Contracts.Responses;
using HealthMed.PacienteService.Application.Exceptions;
using HealthMed.PacienteService.Application.UseCases.Pacientes.Commands.BuscaEspecialidade;
using HealthMed.PacienteService.Application.UseCases.Pacientes.Commands.BuscaMedico;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.PacienteService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PacienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PacienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("BuscaMedico")]
        public async Task<ActionResult> BuscaMedico([FromBody] BuscaMedicoCommandRequest query)
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
        public async Task<ActionResult> BuscaEspecialidade([FromBody] BuscaEspecialidadeCommandRequest query)
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

        //[HttpGet("MedicosDisponiveis")]
        //public async Task<ActionResult> MedicosDisponiveis()
        //{
        //    try
        //    {
        //        var client = _clientFactory.CreateRequestClient<BuscaMedicoEspecialidadeCommand>();
        //        var response = await client.Create<MedicosResponse>();
        //        return Ok(response.Message);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        return BadRequest(ex.Errors);
        //    }
        //}

    }
}
