using HealthMed.BuildingBlocks.Contracts;
using HealthMed.PacienteService.Application.Exceptions;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.PacienteService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PacienteController : ControllerBase
    {
        private readonly IClientFactory _clientFactory;

        public PacienteController(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpPost("BuscaMedico")]
        public async Task<ActionResult> BuscaMedico([FromBody] BuscaMedicoCommand query)
        {
            try
            {
                var client = _clientFactory.CreateRequestClient<BuscaMedicoCommand>();
                var response = await client.GetResponse<MedicosResponse>(query);
                return Ok(response.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpPost("BuscaEspecialidade")]
        public async Task<ActionResult> BuscaEspecialidade([FromBody] BuscaMedicoEspecialidadeCommand query)
        {
            try
            {
                var client = _clientFactory.CreateRequestClient<BuscaMedicoEspecialidadeCommand>();
                var response = await client.GetResponse<MedicosResponse>(query);
                return Ok(response.Message);
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
