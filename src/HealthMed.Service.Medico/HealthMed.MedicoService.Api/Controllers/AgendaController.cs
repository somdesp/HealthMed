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

    [HttpGet("BuscaAgendaDisponivelMedico")]
    [Authorize(Roles = "Medico")]
    public async Task<ActionResult> BuscaAgendaDisponivelMedico()
    {
        try
        {
            var result = await _mediator.Send(new BuscaAgendaDisponivelMedicoQuery { MedicoId = _appUsuario.GetUsuarioId() });
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
    public async Task<ActionResult> DeletaAgenda([FromRoute] int agendaId)
    {
        try
        {
            await _mediator.Send(new DeletaAgendaCommandRequest
            {
                MedicoId = _appUsuario.GetUsuarioId(),
                Id = agendaId
            });
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }

    [HttpGet("BuscaAgendaPorMedicoId/{medicoId:int}")]
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
