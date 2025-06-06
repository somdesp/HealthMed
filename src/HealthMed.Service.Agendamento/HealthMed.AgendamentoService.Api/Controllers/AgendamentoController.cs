﻿using FluentValidation;
using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.AceitaRecusaAgendamento;
using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.CancelaAgendamento;
using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.NovoAgendamento;
using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Queries.BuscaAgendamentosMedico;
using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Queries.BuscaAgendamentosPaciente;
using HealthMed.BuildingBlocks.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace HealthMed.AgendamentoService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgendamentoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IAppUsuario _appUsuario;
    public AgendamentoController(IMediator mediator, IAppUsuario appUsuario)
    {
        _mediator = mediator;
        _appUsuario = appUsuario;
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

    [HttpPost("cancela")]
    [Authorize(Roles = "Paciente")]
    public async Task<ActionResult> CancelaAgendamento(CancelaAgendamentoCommandRequest request)
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

    [HttpPut("AceitaRecusaAgendamento/{agendamentoId:int}")]
    [Authorize(Roles = "Medico")]
    public async Task<ActionResult> AceitaRecusaAgendamento([FromRoute] int agendamentoId, [FromBody] AceitaRecusaAgendamentoCommandRequest request)
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

    [HttpGet("paciente/MeusAgendamentos")]
    [Authorize(Roles = "Paciente")]
    public async Task<ActionResult> AgendamentosPaciente()
    {
        try
        {
            var result = await _mediator.Send(new BuscaAgendamentosPacienteQuery { PacienteId = _appUsuario.GetUsuarioId() });
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }

    [HttpGet("medico/MeusAgendamentos")]
    [Authorize(Roles = "Medico")]
    public async Task<ActionResult> AgendamentosMedico()
    {
        try
        {
            var result = await _mediator.Send(new BuscaAgendamentosMedicoQuery { MedicoId = _appUsuario.GetUsuarioId() });
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
}
