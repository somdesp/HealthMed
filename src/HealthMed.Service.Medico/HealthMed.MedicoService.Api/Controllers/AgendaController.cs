using FluentValidation;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.AlteraAgenda;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.NovaAgenda;
using HealthMed.MedicoService.Application.UseCases.Agendas.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.MedicoService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgendaController : ControllerBase
{
    private readonly IMediator _mediator;

    public AgendaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Medico")]
    public async Task<ActionResult> BuscaAgendas(int medicoId)
    {
        try
        {
            var result = await _mediator.Send(new AgendaDisponivelQuery { MedicoId = medicoId });
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Medico")]
    public async Task<ActionResult> NovaAgenda(NovaAgendaCommandRequest request)
    {
        try
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }

    [HttpPut("{agendaId:int}")]
    [Authorize(Roles = "Medico")]
    public async Task<ActionResult> AlteraAgenda([FromRoute] int agendaId, [FromBody] AlteraAgendaCommandRequest request)
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
