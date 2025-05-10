using AutoMapper;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.NovaAgenda;
using HealthMed.MedicoServiceService.Domain.Entities;
using Moq;

namespace HealthMed.MedicoService.Tests.UseCases.Agendas.Commands;

public class NovaAgendaCommandRequestHandlerTests
{
    private readonly Mock<IAgendaRepository> _agendaRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly NovaAgendaCommandRequestHandler _handler;

    public NovaAgendaCommandRequestHandlerTests()
    {
        _agendaRepositoryMock = new Mock<IAgendaRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new NovaAgendaCommandRequestHandler(_agendaRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenAgendaIsAddedSuccessfully()
    {
        // Arrange
        var request = new NovaAgendaCommandRequest
        {
            MedicoId = 1
        };

        var agendaEntity = new AgendaMedico
        {
            Id = 1,
            MedicoId = request.MedicoId
        };

        _mapperMock
            .Setup(mapper => mapper.Map<AgendaMedico>(request))
            .Returns(agendaEntity);

        _agendaRepositoryMock
            .Setup(repo => repo.AddAsync(agendaEntity))
            .ReturnsAsync(agendaEntity);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result);
        _mapperMock.Verify(mapper => mapper.Map<AgendaMedico>(request), Times.Once);
        _agendaRepositoryMock.Verify(repo => repo.AddAsync(agendaEntity), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFalse_WhenAgendaIsNotAdded()
    {
        // Arrange
        var request = new NovaAgendaCommandRequest
        {
            MedicoId = 1
        };

        var agendaEntity = new AgendaMedico
        {
            MedicoId = request.MedicoId
        };

        _mapperMock
            .Setup(mapper => mapper.Map<AgendaMedico>(request))
            .Returns(agendaEntity);

        _agendaRepositoryMock
            .Setup(repo => repo.AddAsync(agendaEntity))
            .ReturnsAsync((AgendaMedico)null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result);
        _mapperMock.Verify(mapper => mapper.Map<AgendaMedico>(request), Times.Once);
        _agendaRepositoryMock.Verify(repo => repo.AddAsync(agendaEntity), Times.Once);
    }
}
