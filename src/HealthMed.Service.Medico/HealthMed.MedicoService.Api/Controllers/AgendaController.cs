using FluentValidation;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.DeletaAgenda;
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
    private readonly IAppUsuario _appUsuario;

    public AgendaController(IMediator mediator, IAppUsuario appUsuario)
    {
        _mediator = mediator;
        _appUsuario = appUsuario;
    }

    [HttpGet("minha-agendas")]
    [Authorize(Roles = "Medico")]
    public async Task<ActionResult> BuscaMinhasAgendas()
    {
        try
        {
            var result = await _mediator.Send(new MinhaAgendaQuery { MedicoId = _appUsuario.GetUsuarioId() });
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

    [HttpDelete("{agendaId:int}")]
    [Authorize(Roles = "Medico")]
    public async Task<ActionResult> DeletaAgenda([FromRoute] int agendaId, [FromBody] DeletaAgendaCommandRequest request)
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

    [HttpGet("agenda/{medicoId:int}")]
    [Authorize(Roles = "Paciente")]
    public async Task<ActionResult> BuscaAgendaPorMedicoId(int medicoId)
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
}
