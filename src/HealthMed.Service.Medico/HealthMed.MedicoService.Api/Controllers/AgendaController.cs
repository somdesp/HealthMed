using FluentValidation;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.AlteraAgenda;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.NovaAgenda;
using HealthMed.MedicoService.Application.UseCases.Agendas.Queries;
using HealthMed.MedicoService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.MedicoService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgendaController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IAppUsuario _appUsuario;

    public AgendaController(IMediator mediator, IAppUsuario appUsuario)
    {
        _mediator = mediator;
        _appUsuario = appUsuario;
    }

    [HttpGet]
    [Authorize(Roles = "Medico")]
    public async Task<ActionResult> BuscaAgendas()
    {
        try
        {
            var result = await _mediator.Send(new AgendaDisponivelQuery { MedicoId = _appUsuario.GetUsuarioId() });
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
            request.MedicoId = _appUsuario.GetUsuarioId();
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
            request.Id = agendaId;
            request.MedicoId = _appUsuario.GetUsuarioId();
            await _mediator.Send(request);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
}
