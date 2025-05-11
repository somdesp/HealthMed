using AutoMapper;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.CancelaAgendamento;
using HealthMed.AgendamentoService.Domain.Entities;
using HealthMed.AgendamentoService.Domain.Entities.Enums;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Messaging;
using MassTransit;
using Moq;

namespace HealthMed.Tests.AgendamentoServiceApplication;

public class CancelaAgendamentoCommandRequestHandlerTests
{
    private readonly Mock<IAgendamentoRepository> _agendamentoRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IAppUsuario> _appUsuarioMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly CancelaAgendamentoCommandRequestHandler _handler;

    public CancelaAgendamentoCommandRequestHandlerTests()
    {
        _agendamentoRepositoryMock = new Mock<IAgendamentoRepository>();
        _mapperMock = new Mock<IMapper>();
        _appUsuarioMock = new Mock<IAppUsuario>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();

        _handler = new CancelaAgendamentoCommandRequestHandler(
            _agendamentoRepositoryMock.Object,
            _mapperMock.Object,
            _appUsuarioMock.Object,
            _publishEndpointMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldCancelAgendamento_WhenAgendamentoExists()
    {
        // Arrange
        var usuarioId = 1;
        var agendamentoId = 10;
        var motivoCancelamento = "Paciente indisponível";

        var request = new CancelaAgendamentoCommandRequest
        {
            AgendamentoId = agendamentoId,
            MotivoCancelamento = motivoCancelamento
        };

        var agendamento = new Agendamento
        {
            Id = agendamentoId,
            MotivoCancelamento = "Paciente indisponível",
            PacienteId = usuarioId,
            Status = AgendamentoStatus.Cancelado
        };

        _appUsuarioMock.Setup(u => u.GetUsuarioId()).Returns(usuarioId);

        _agendamentoRepositoryMock
            .Setup(repo => repo.FirstOrDefaultAsync(a => a.Id == agendamentoId && a.PacienteId == usuarioId))
            .ReturnsAsync(agendamento);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(AgendamentoStatus.Cancelado, agendamento.Status);
        Assert.Equal(motivoCancelamento, agendamento.MotivoCancelamento);
    }

    [Fact]
    public async Task Handle_ShouldNotCancelAgendamento_WhenAgendamentoDoesNotExist()
    {
        // Arrange
        var usuarioId = 1;
        var agendamentoId = 10;

        var request = new CancelaAgendamentoCommandRequest
        {
            AgendamentoId = agendamentoId,
            MotivoCancelamento = "Paciente indisponível"
        };

        _appUsuarioMock.Setup(u => u.GetUsuarioId()).Returns(usuarioId);

        _agendamentoRepositoryMock
            .Setup(repo => repo.FirstOrDefaultAsync(a => a.Id == agendamentoId && a.PacienteId == usuarioId))
            .ReturnsAsync((Agendamento)null);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        _agendamentoRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Agendamento>()), Times.Never);
        _publishEndpointMock.Verify(endpoint => endpoint.Publish(It.IsAny<AgendamentoCanceladoEvent>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
