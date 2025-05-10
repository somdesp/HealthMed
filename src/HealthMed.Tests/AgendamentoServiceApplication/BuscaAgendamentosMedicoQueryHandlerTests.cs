using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Application.Dtos;
using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Queries.BuscaAgendamentosMedico;
using HealthMed.AgendamentoService.Domain.Entities;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Contracts.Requests;
using HealthMed.BuildingBlocks.Contracts.Responses;
using MassTransit;
using Moq;
using Xunit;

namespace HealthMed.AgendamentoService.Tests.UseCases.Agendamentos.Queries
{
    public class BuscaAgendamentosMedicoQueryHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAgendamentoRepository> _agendamentoRepositoryMock;
        private readonly Mock<IRequestClient<BuscaAgendasMedicoRequest>> _requestClientMock;
        private readonly BuscaAgendamentosMedicoQueryHandler _handler;

        public BuscaAgendamentosMedicoQueryHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _agendamentoRepositoryMock = new Mock<IAgendamentoRepository>();
            _requestClientMock = new Mock<IRequestClient<BuscaAgendasMedicoRequest>>();
            _handler = new BuscaAgendamentosMedicoQueryHandler(
                Mock.Of<IAppUsuario>(), // Não utilizado diretamente no método
                _mapperMock.Object,
                _agendamentoRepositoryMock.Object,
                _requestClientMock.Object
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
                Agendas =
                [
                    new() { Id = 10 },
                    new() { Id = 20 }
                ]
            };

            var agendamentos = new List<Agendamento>
            {
                new() { Id = 1, AgendaId = 10 },
                new() { Id = 2, AgendaId = 20 }
            };

            var mappedAgendamentos = new List<MeusAgendamentosResponseDto>
            {
                new() { Id = 1, DataHora = new DateTime(2025, 12, 31) },
                new() { Id = 2, DataHora = new DateTime(2025, 12, 31) }
            };

            _requestClientMock
                .Setup(client => client.GetResponse<BuscaAgendasMedicoResponse>(It.IsAny<BuscaAgendasMedicoRequest>(), default, default))
                .ReturnsAsync(Mock.Of<Response<BuscaAgendasMedicoResponse>>(r => r.Message == agendasResponse));

            _agendamentoRepositoryMock
                .Setup(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Agendamento, bool>>>()))
                .ReturnsAsync(agendamentos);

            _mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<MeusAgendamentosResponseDto>>(agendamentos))
                .Returns(mappedAgendamentos);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mappedAgendamentos, result);

            _requestClientMock.Verify(client => client.GetResponse<BuscaAgendasMedicoResponse>(
                It.Is<BuscaAgendasMedicoRequest>(r => r.MedicoId == medicoId), default, default), Times.Once);

            _agendamentoRepositoryMock.Verify(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Agendamento, bool>>>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<MeusAgendamentosResponseDto>>(agendamentos), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoAgendasExist()
        {
            // Arrange
            var medicoId = 1;
            var request = new BuscaAgendamentosMedicoQuery { MedicoId = medicoId };

            var agendasResponse = new BuscaAgendasMedicoResponse
            {
                Agendas = new List<AgendaResponse>()
            };

            _requestClientMock
                .Setup(client => client.GetResponse<BuscaAgendasMedicoResponse>(It.IsAny<BuscaAgendasMedicoRequest>(), default, default))
                .ReturnsAsync(Mock.Of<Response<BuscaAgendasMedicoResponse>>(r => r.Message == agendasResponse));

            _agendamentoRepositoryMock
                .Setup(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Agendamento, bool>>>()))
                .ReturnsAsync(new List<Agendamento>());

            _mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<MeusAgendamentosResponseDto>>(It.IsAny<IEnumerable<Agendamento>>()))
                .Returns(new List<MeusAgendamentosResponseDto>());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

            _requestClientMock.Verify(client => client.GetResponse<BuscaAgendasMedicoResponse>(
                It.Is<BuscaAgendasMedicoRequest>(r => r.MedicoId == medicoId), default, default), Times.Once);

            _agendamentoRepositoryMock.Verify(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Agendamento, bool>>>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<MeusAgendamentosResponseDto>>(It.IsAny<IEnumerable<Agendamento>>()), Times.Once);
        }
    }
}
