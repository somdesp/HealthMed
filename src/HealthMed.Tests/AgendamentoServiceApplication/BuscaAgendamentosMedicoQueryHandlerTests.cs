using AutoMapper;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Queries.BuscaAgendamentosMedico;
using HealthMed.AgendamentoService.Domain.Entities;
using HealthMed.AgendamentoService.Domain.Entities.Enums;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Contracts.Requests;
using HealthMed.BuildingBlocks.Contracts.Responses;
using MassTransit;
using Moq;

namespace HealthMed.Tests.AgendamentoServiceApplication;

public class BuscaAgendamentosMedicoQueryHandlerTests
{
    private readonly Mock<IAppUsuario> _appUsuarioMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IAgendamentoRepository> _agendamentoRepositoryMock;
    private readonly Mock<IRequestClient<BuscaAgendasMedicoRequest>> _requestClientMock;
    private readonly Mock<IRequestClient<PacientesRequest>> _pacienteRequestClientMock;
    private readonly BuscaAgendamentosMedicoQueryHandler _handler;

    public BuscaAgendamentosMedicoQueryHandlerTests()
    {
        _appUsuarioMock = new Mock<IAppUsuario>();
        _mapperMock = new Mock<IMapper>();
        _agendamentoRepositoryMock = new Mock<IAgendamentoRepository>();
        _requestClientMock = new Mock<IRequestClient<BuscaAgendasMedicoRequest>>();
        _pacienteRequestClientMock = new Mock<IRequestClient<PacientesRequest>>();

        _handler = new BuscaAgendamentosMedicoQueryHandler(
            _appUsuarioMock.Object,
            _mapperMock.Object,
            _agendamentoRepositoryMock.Object,
            _requestClientMock.Object,
            _pacienteRequestClientMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedAgendamentos_WhenAgendasExist()
    {
        // Arrange
        var medicoId = 1;
        var request = new BuscaAgendamentosMedicoQuery { MedicoId = medicoId };

        var agendasResponse = new BuscaAgendasMedicoResponse
        {
            Agendas = new List<AgendaResponse>
            {
                new AgendaResponse { Id = 10, DataHora = new System.DateTime(2025, 12, 31) },
                new AgendaResponse { Id = 20, DataHora = new System.DateTime(2025, 12, 31) }
            }
        };

        var agendamentos = new List<Agendamento>
        {
            new Agendamento { Id = 1, AgendaId = 10, PacienteId = 100, Status = AgendamentoStatus.Pendente },
            new Agendamento { Id = 2, AgendaId = 20, PacienteId = 200, Status = AgendamentoStatus.Confirmado }
        };

        var pacientesResponse = new PacientesResponse(
            new List<PacienteResponse>
            {
                new PacienteResponse(100, "Paciente 1"),
                new PacienteResponse (200, "Paciente 2")
            }
        );

        _requestClientMock
            .Setup(client => client.GetResponse<BuscaAgendasMedicoResponse>(It.IsAny<BuscaAgendasMedicoRequest>(), default, default))
            .ReturnsAsync(Mock.Of<Response<BuscaAgendasMedicoResponse>>(r => r.Message == agendasResponse));

        _agendamentoRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Agendamento, bool>>>()))
            .ReturnsAsync(agendamentos);

        _pacienteRequestClientMock
            .Setup(client => client.GetResponse<PacientesResponse>(It.IsAny<PacientesRequest>(), default, default))
            .ReturnsAsync(Mock.Of<Response<PacientesResponse>>(r => r.Message == pacientesResponse));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        var resultList = result.ToList();
        Assert.Equal(1, resultList[0].Id);
        Assert.Equal("Paciente 1", resultList[0].Paciente);
        Assert.Equal(10, resultList[0].AgendaId);
        Assert.Equal("Pendente", resultList[0].Status);

        Assert.Equal(2, resultList[1].Id);
        Assert.Equal("Paciente 2", resultList[1].Paciente);
        Assert.Equal(20, resultList[1].AgendaId);
        Assert.Equal("Confirmado", resultList[1].Status);

        _requestClientMock.Verify(client => client.GetResponse<BuscaAgendasMedicoResponse>(
            It.Is<BuscaAgendasMedicoRequest>(r => r.MedicoId == medicoId), default, default), Times.Once);

        _agendamentoRepositoryMock.Verify(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Agendamento, bool>>>()), Times.Once);

        _pacienteRequestClientMock.Verify(client => client.GetResponse<PacientesResponse>(
            It.IsAny<PacientesRequest>(), default, default), Times.Once);
    }
}
