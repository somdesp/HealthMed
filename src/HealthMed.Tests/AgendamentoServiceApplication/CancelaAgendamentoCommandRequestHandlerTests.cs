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
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly CancelaAgendamentoCommandRequestHandler _handler;

    public CancelaAgendamentoCommandRequestHandlerTests()
    {
        _agendamentoRepositoryMock = new Mock<IAgendamentoRepository>();
        _mapperMock = new Mock<IMapper>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _handler = new CancelaAgendamentoCommandRequestHandler(
            _agendamentoRepositoryMock.Object,
            _mapperMock.Object,
            Mock.Of<IAppUsuario>(),
            _publishEndpointMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldCancelAgendamentoAndPublishEvent()
    {
        // Arrange
        var request = new CancelaAgendamentoCommandRequest
        {
            AgendamentoId = 1,
            MotivoCancelamento = "Motivo de teste",
        };

        var agendamento = new Agendamento
        {
            Id = request.AgendamentoId,
            Status = AgendamentoStatus.Cancelado
        };

        _mapperMock
            .Setup(mapper => mapper.Map<Agendamento>(request))
            .Returns(agendamento);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(AgendamentoStatus.Cancelado, agendamento.Status);

        _agendamentoRepositoryMock.Verify(repo => repo.UpdateAsync(agendamento), Times.Once);
        _publishEndpointMock.Verify(endpoint => endpoint.Publish(
            It.Is<AgendamentoCanceladoEvent>(e => e.AgendamentoId == agendamento.Id && e.AgendaId == agendamento.AgendaId),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
