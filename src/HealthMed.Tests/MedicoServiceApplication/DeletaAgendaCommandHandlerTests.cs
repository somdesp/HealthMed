using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.DeletaAgenda;
using HealthMed.MedicoServiceService.Domain.Entities;
using MassTransit;
using MassTransit.Transports;
using Moq;
using Xunit;

namespace HealthMed.MedicoService.Tests.UseCases.Agendas.Commands
{
    public class DeletaAgendaCommandHandlerTests
    {
        private readonly Mock<IAgendaRepository> _agendaRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPublishEndpoint> _publishEndpoint;

        private readonly DeletaAgendaCommandHandler _handler;

        public DeletaAgendaCommandHandlerTests()
        {
            _agendaRepositoryMock = new Mock<IAgendaRepository>();
            _mapperMock = new Mock<IMapper>();
            _publishEndpoint = new Mock<IPublishEndpoint>();
            _handler = new DeletaAgendaCommandHandler(_agendaRepositoryMock.Object, _mapperMock.Object, _publishEndpoint.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateAsync_WhenRequestIsValid()
        {
            // Arrange
            var request = new DeletaAgendaCommandRequest
            {
                Id = 1
            };

            var agendaEntity = new AgendaMedico
            {
                Id = request.Id
            };

            _mapperMock
                .Setup(mapper => mapper.Map<AgendaMedico>(request))
                .Returns(agendaEntity);

            _agendaRepositoryMock
                .Setup(repo => repo.UpdateAsync(agendaEntity))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _mapperMock.Verify(mapper => mapper.Map<AgendaMedico>(request), Times.Once);
            _agendaRepositoryMock.Verify(repo => repo.UpdateAsync(agendaEntity), Times.Once);
        }
    }
}
