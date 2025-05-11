using AutoMapper;
using HealthMed.BuildingBlocks.Contracts.Responses;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.Dtos;
using HealthMed.MedicoService.Application.UseCases.Agendas.Queries;
using HealthMed.MedicoServiceService.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace HealthMed.MedicoService.Tests.UseCases.Agendas.Queries;

public class MinhaAgendaQueryHandlerTests
{
    private readonly Mock<IAgendaRepository> _agendaRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly BuscaAgendaReservadaMedicoQueryHandler _handler;

    public MinhaAgendaQueryHandlerTests()
    {
        _agendaRepositoryMock = new Mock<IAgendaRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new BuscaAgendaReservadaMedicoQueryHandler(_agendaRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedResponse_WhenAgendasExist()
    {
        // Arrange
        var medicoId = 1;
        var request = new BuscaAgendaReservadaMedicoQuery { MedicoId = medicoId };

        var agendaEntities = new List<AgendaMedico>
        {
            new() { Id = 1, MedicoId = medicoId, DataHora = new DateTime(2025, 12, 31) },
            new() { Id = 2, MedicoId = medicoId, DataHora = new DateTime(2025, 12, 31) }
        };

        // Fix for CS9035: Add the required 'Medico' property initialization in the object initializers.

        var agendaDtos = new List<AgendaDisponivelDto>
        {
           new() { Id = 1, MedicoId = medicoId, DataHora = new DateTime(2025, 12, 31), Medico = "Dr. Example", ValorConsulta = 100.0 },
           new() { Id = 2, MedicoId = medicoId, DataHora = new DateTime(2025, 12, 31), Medico = "Dr. Example", ValorConsulta = 100.0 }
        };

        var agendaResponses = new List<AgendaResponse>
        {
            new() { Id = 1, DataHora = new DateTime(2025, 12, 31) },
            new() { Id = 2, DataHora = new DateTime(2025, 12, 31) }
        };

        _agendaRepositoryMock
           .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<AgendaMedico, bool>>>()))
           .ReturnsAsync(agendaEntities);

        _mapperMock
            .Setup(mapper => mapper.Map<IEnumerable<AgendaDisponivelDto>>(agendaEntities))
            .Returns(agendaDtos);

        _mapperMock
            .Setup(mapper => mapper.Map<IEnumerable<AgendaResponse>>(agendaDtos))
            .Returns(agendaResponses);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(agendaResponses, result.Agendas);

        _mapperMock.Verify(mapper => mapper.Map<IEnumerable<AgendaDisponivelDto>>(agendaEntities), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<IEnumerable<AgendaResponse>>(agendaDtos), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyResponse_WhenNoAgendasExist()
    {
        // Arrange
        var medicoId = 1;
        var request = new BuscaAgendaReservadaMedicoQuery { MedicoId = medicoId };

        _agendaRepositoryMock
            .Setup(repo => repo.GetAsync(a => a.MedicoId == medicoId))
            .ReturnsAsync(new List<AgendaMedico>());

        _mapperMock
            .Setup(mapper => mapper.Map<IEnumerable<AgendaDisponivelDto>>(It.IsAny<IEnumerable<AgendaMedico>>()))
            .Returns(new List<AgendaDisponivelDto>());

        _mapperMock
            .Setup(mapper => mapper.Map<IEnumerable<AgendaResponse>>(It.IsAny<IEnumerable<AgendaDisponivelDto>>()))
            .Returns(new List<AgendaResponse>());

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Agendas);

        _mapperMock.Verify(mapper => mapper.Map<IEnumerable<AgendaDisponivelDto>>(It.IsAny<IEnumerable<AgendaMedico>>()), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<IEnumerable<AgendaResponse>>(It.IsAny<IEnumerable<AgendaDisponivelDto>>()), Times.Once);
    }
}
