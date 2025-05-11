using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.DeletaAgenda;
using HealthMed.MedicoService.Domain.Entities;
using Moq;

namespace HealthMed.Tests.MedicoServiceApplication;

public class DeletaAgendaCommandHandlerTests
{
    private readonly Mock<IAgendaRepository> _agendaRepositoryMock;
    private readonly DeletaAgendaCommandHandler _handler;

    public DeletaAgendaCommandHandlerTests()
    {
        _agendaRepositoryMock = new Mock<IAgendaRepository>();
        _handler = new DeletaAgendaCommandHandler(
            _agendaRepositoryMock.Object,
            null,
            null
        );
    }

    [Fact]
    public async Task Handle_ShouldDeleteAgenda_WhenAgendaExistsAndIsNotReserved()
    {
        // Arrange
        var request = new DeletaAgendaCommandRequest
        {
            Id = 1,
            MedicoId = 2
        };

        var agenda = new AgendaMedico
        {
            Id = 1,
            MedicoId = 2,
            Reservada = false
        };

        _agendaRepositoryMock
            .Setup(repo => repo.FirstOrDefaultAsync(
                x => x.Id == request.Id && x.MedicoId == request.MedicoId && !x.Reservada))
            .ReturnsAsync(agenda);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        _agendaRepositoryMock.Verify(repo => repo.DeleteAsync(agenda), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldNotDeleteAgenda_WhenAgendaDoesNotExist()
    {
        // Arrange
        var request = new DeletaAgendaCommandRequest
        {
            Id = 1,
            MedicoId = 2
        };

        _agendaRepositoryMock
            .Setup(repo => repo.FirstOrDefaultAsync(
                x => x.Id == request.Id && x.MedicoId == request.MedicoId && !x.Reservada))
            .ReturnsAsync((AgendaMedico)null);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        _agendaRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<AgendaMedico>()), Times.Never);
    }
}
