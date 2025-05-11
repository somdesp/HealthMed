using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.Dtos;
using HealthMed.MedicoService.Application.UseCases.Medicos.Commands.BuscaMedico;
using HealthMed.MedicoService.Domain.Entities;
using Moq;
using Xunit;

namespace HealthMed.Tests.MedicoServiceApplication
{
    public class BuscaMedicoPorNomeCommandHandlerTests
    {
        private readonly Mock<IMedicoRepository> _medicoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BuscaMedicoPorNomeCommandHandler _handler;

        public BuscaMedicoPorNomeCommandHandlerTests()
        {
            _medicoRepositoryMock = new Mock<IMedicoRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new BuscaMedicoPorNomeCommandHandler(_medicoRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedMedicoDtos_WhenMedicoExists()
        {
            // Arrange
            var nomeMedico = "Dr. João";
            var request = new BuscaMedicoPorNomeCommantRequest { NomeMedico = nomeMedico };

            var medicoEntities = new List<Medico> { new Medico { Id = 1, Nome = "Dr. João", Crm = "123", Senha = "abc123", ValorConsulta = 100 } };
            var medicoDtos = new List<MedicoDto> { new MedicoDto { Nome = "Dr. João", Crm = "123", ValorConsulta = 100 } };

            _medicoRepositoryMock
                .Setup(repo => repo.BuscaMedico(nomeMedico))
                .ReturnsAsync(medicoEntities);

            _mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<MedicoDto>>(medicoEntities))
                .Returns(medicoDtos);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(medicoDtos, result);

            _medicoRepositoryMock.Verify(repo => repo.BuscaMedico(nomeMedico), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<MedicoDto>>(medicoEntities), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoMedicoFound()
        {
            // Arrange
            var nomeMedico = "Dr. Inexistente";
            var request = new BuscaMedicoPorNomeCommantRequest { NomeMedico = nomeMedico };

            _medicoRepositoryMock
                .Setup(repo => repo.BuscaMedico(nomeMedico))
                .ReturnsAsync(new List<Medico>());

            _mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<MedicoDto>>(It.IsAny<IEnumerable<Medico>>()))
                .Returns(new List<MedicoDto>());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

            _medicoRepositoryMock.Verify(repo => repo.BuscaMedico(nomeMedico), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<MedicoDto>>(It.IsAny<IEnumerable<Medico>>()), Times.Once);
        }
    }
}
