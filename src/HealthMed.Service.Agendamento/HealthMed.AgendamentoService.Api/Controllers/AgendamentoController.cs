using HealthMed.AgendamentoService.Application.Exceptions;
using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.NovoAgendamento;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.AgendamentoService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgendamentoController : ControllerBase
{
    private readonly IMediator _mediator;

    public AgendamentoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Paciente")]
    public async Task<ActionResult> NovoAgendamento(NovoAgendamentoCommandRequest request)
    {
        try
        {
            await _mediator.Send(request);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
}
