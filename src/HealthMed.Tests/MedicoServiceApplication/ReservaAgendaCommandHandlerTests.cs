using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.ReservaAgenda;
using HealthMed.MedicoService.Domain.Entities;
using Moq;
using Xunit;

namespace HealthMed.Tests.MedicoServiceApplication;

public class ReservaAgendaCommandHandlerTests
{
    private readonly Mock<IAgendaRepository> _agendaRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ReservaAgendaCommandHandler _handler;

    public ReservaAgendaCommandHandlerTests()
    {
        _agendaRepositoryMock = new Mock<IAgendaRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new ReservaAgendaCommandHandler(_agendaRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateAgenda_WhenAgendaExists()
    {
        // Arrange
        var request = new ReservaAgendaCommandRequest
        {
            AgendaId = 1,
            ReservaAgenda = true
        };

        var agendaEntity = new AgendaMedico
        {
            Id = request.AgendaId,
            Reservada = false
        };

        _agendaRepositoryMock
            .Setup(repo => repo.GetByIdAsync(request.AgendaId))
            .ReturnsAsync(agendaEntity);

        _agendaRepositoryMock
            .Setup(repo => repo.UpdateAsync(agendaEntity))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(agendaEntity.Reservada);
        _agendaRepositoryMock.Verify(repo => repo.GetByIdAsync(request.AgendaId), Times.Once);
        _agendaRepositoryMock.Verify(repo => repo.UpdateAsync(agendaEntity), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldNotUpdateAgenda_WhenAgendaDoesNotExist()
    {
        // Arrange
        var request = new ReservaAgendaCommandRequest
        {
            AgendaId = 1,
            ReservaAgenda = true
        };

        _agendaRepositoryMock
            .Setup(repo => repo.GetByIdAsync(request.AgendaId))
            .ReturnsAsync((AgendaMedico)null);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        _agendaRepositoryMock.Verify(repo => repo.GetByIdAsync(request.AgendaId), Times.Once);
        _agendaRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<AgendaMedico>()), Times.Never);
    }
}
