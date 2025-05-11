using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.AceitaRecusaAgendamento;
using HealthMed.AgendamentoService.Domain.Entities;
using HealthMed.AgendamentoService.Domain.Entities.Enums;
using HealthMed.BuildingBlocks.Messaging;
using MassTransit;
using Moq;

namespace HealthMed.Tests.AgendamentoServiceApplication;

public class AceitaRecusaAgendamentoCommandRequestHandlerTests
{
    private readonly Mock<IAgendamentoRepository> _agendamentoRepositoryMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly AceitaRecusaAgendamentoCommandRequestHandler _handler;

    public AceitaRecusaAgendamentoCommandRequestHandlerTests()
    {
        _agendamentoRepositoryMock = new Mock<IAgendamentoRepository>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _handler = new AceitaRecusaAgendamentoCommandRequestHandler(_agendamentoRepositoryMock.Object, _publishEndpointMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldConfirmAgendamento_WhenAceitaAgendamentoIsTrue()
    {
        // Arrange
        var request = new AceitaRecusaAgendamentoCommandRequest
        {
            AgendamentoId = 1,
            AceitaAgendamento = true
        };

        var agendamento = new Agendamento
        {
            Id = request.AgendamentoId,
            Status = AgendamentoStatus.Pendente
        };

        _agendamentoRepositoryMock
            .Setup(repo => repo.GetByIdAsync(request.AgendamentoId))
            .ReturnsAsync(agendamento);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(AgendamentoStatus.Confirmado, agendamento.Status);
        _agendamentoRepositoryMock.Verify(repo => repo.UpdateAsync(agendamento), Times.Once);
        _publishEndpointMock.Verify(endpoint => endpoint.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldRecusarAgendamentoAndPublishEvent_WhenAceitaAgendamentoIsFalse()
    {
        // Arrange
        var request = new AceitaRecusaAgendamentoCommandRequest
        {
            AgendamentoId = 1,
            AceitaAgendamento = false
        };

        var agendamento = new Agendamento
        {
            Id = request.AgendamentoId,
            AgendaId = 10,
            Status = AgendamentoStatus.Pendente
        };

        _agendamentoRepositoryMock
            .Setup(repo => repo.GetByIdAsync(request.AgendamentoId))
            .ReturnsAsync(agendamento);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(AgendamentoStatus.Recusado, agendamento.Status);
        _agendamentoRepositoryMock.Verify(repo => repo.UpdateAsync(agendamento), Times.Once);
        _publishEndpointMock.Verify(endpoint => endpoint.Publish(
            It.Is<AgendamentoRecusadoEvent>(e => e.AgendamentoId == agendamento.Id && e.AgendaId == agendamento.AgendaId),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldDoNothing_WhenAgendamentoDoesNotExist()
    {
        // Arrange
        var request = new AceitaRecusaAgendamentoCommandRequest
        {
            AgendamentoId = 1,
            AceitaAgendamento = true
        };

        _agendamentoRepositoryMock
            .Setup(repo => repo.GetByIdAsync(request.AgendamentoId))
            .ReturnsAsync((Agendamento)null);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        _agendamentoRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Agendamento>()), Times.Never);
        _publishEndpointMock.Verify(endpoint => endpoint.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
