using AutoMapper;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.NovoAgendamento;
using HealthMed.AgendamentoService.Domain.Entities;
using HealthMed.AgendamentoService.Domain.Entities.Enums;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Messaging;
using MassTransit;
using Moq;

namespace HealthMed.AgendamentoService.Tests.UseCases.Agendamentos.Commands;

public class NovoAgendamentoCommandRequestHandlerTests
{
    private readonly Mock<IAgendamentoRepository> _agendamentoRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IAppUsuario> _appUsuarioMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly NovoAgendamentoCommandRequestHandler _handler;

    public NovoAgendamentoCommandRequestHandlerTests()
    {
        _agendamentoRepositoryMock = new Mock<IAgendamentoRepository>();
        _mapperMock = new Mock<IMapper>();
        _appUsuarioMock = new Mock<IAppUsuario>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _handler = new NovoAgendamentoCommandRequestHandler(
            _agendamentoRepositoryMock.Object,
            _mapperMock.Object,
            _appUsuarioMock.Object,
            _publishEndpointMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldCreateAgendamentoAndPublishEvent()
    {
        // Arrange
        var request = new NovoAgendamentoCommandRequest
        {
            AgendaId = 10,
        };

        var usuarioId = 123; // Simula o ID do usuário logado
        var novoAgendamento = new Agendamento
        {
            AgendaId = request.AgendaId,
            Status = AgendamentoStatus.Pendente,
            PacienteId = usuarioId
        };

        _mapperMock
            .Setup(mapper => mapper.Map<Agendamento>(request))
            .Returns(novoAgendamento);

        _appUsuarioMock
            .Setup(appUsuario => appUsuario.GetUsuarioId())
            .Returns(usuarioId);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        _agendamentoRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Agendamento>(a =>
            a.AgendaId == request.AgendaId &&
            a.Status == AgendamentoStatus.Pendente &&
            a.PacienteId == usuarioId
        )), Times.Once);
    }
}
