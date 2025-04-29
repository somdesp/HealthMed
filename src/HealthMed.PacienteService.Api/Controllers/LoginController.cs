using HealthMed.PacienteService.Application.UseCases.Pacientes.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.PacienteService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> Login([FromBody] LoginQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
