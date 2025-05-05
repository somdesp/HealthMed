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
        private readonly IRequestClient<BuscaMedicoCommand> _requestClient;

        public PacienteController(IRequestClient<BuscaMedicoCommand> requestClient)
        {
            _requestClient = requestClient;
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] BuscaMedicoCommand query)
        {
            try
            {
                var response = await _requestClient.GetResponse<MedicoResponse>(query);
                return Ok(response.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors["Login"]);
            }
        }
    }
}
