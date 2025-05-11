using AutoMapper;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.Dtos;
using HealthMed.MedicoService.Application.UseCases.Agendas.Queries;
using HealthMed.MedicoService.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace HealthMed.Tests.MedicoServiceApplication;

public class AgendaDisponivelQueryHandlerTests
{
    private readonly Mock<IAgendaRepository> _agendaRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AgendaDisponivelQueryHandler _handler;

    public AgendaDisponivelQueryHandlerTests()
    {
        _agendaRepositoryMock = new Mock<IAgendaRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new AgendaDisponivelQueryHandler(_agendaRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAvailableAgendas_WhenAgendasExist()
    {
        // Arrange  
        var medicoId = 1;
        var request = new AgendaDisponivelQuery { MedicoId = medicoId };

        var agendaEntities = new List<AgendaMedico>
       {
           new() { Id = 1, MedicoId = medicoId, Reservada = false },
           new() { Id = 2, MedicoId = medicoId, Reservada = false }
       };

        var agendaDtos = new List<AgendaDisponivelDto>
       {
           new() { Id = 1, MedicoId = medicoId, Medico = "Dr. Example", DataHora = DateTime.Now, ValorConsulta = 100.0 },
           new() { Id = 2, MedicoId = medicoId, Medico = "Dr. Example", DataHora = DateTime.Now, ValorConsulta = 100.0 }
       };

        _agendaRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<AgendaMedico, bool>>>()))
            .ReturnsAsync(agendaEntities);

        _mapperMock
            .Setup(mapper => mapper.Map<IEnumerable<AgendaDisponivelDto>>(agendaEntities))
            .Returns(agendaDtos);

        // Act  
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert  
        Assert.NotNull(result);
        Assert.Equal(agendaDtos.Count, result.Count());
        Assert.Equal(agendaDtos, result);

        _mapperMock.Verify(mapper => mapper.Map<IEnumerable<AgendaDisponivelDto>>(agendaEntities), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoAvailableAgendasExist()
    {
        // Arrange
        var medicoId = 1;
        var request = new AgendaDisponivelQuery { MedicoId = medicoId };

        _agendaRepositoryMock
            .Setup(repo => repo.GetAsync(a => a.MedicoId == medicoId && !a.Reservada))
            .ReturnsAsync(new List<AgendaMedico>());

        _mapperMock
            .Setup(mapper => mapper.Map<IEnumerable<AgendaDisponivelDto>>(It.IsAny<IEnumerable<AgendaMedico>>()))
            .Returns(new List<AgendaDisponivelDto>());

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);

        _mapperMock.Verify(mapper => mapper.Map<IEnumerable<AgendaDisponivelDto>>(It.IsAny<IEnumerable<AgendaMedico>>()), Times.Once);
    }
}
