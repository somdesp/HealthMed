using AutoMapper;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.Dtos;
using HealthMed.MedicoService.Application.UseCases.Medicos.Commands.BuscaEspecialidade;
using HealthMed.MedicoService.Domain.Entities;
using Moq;

namespace HealthMed.Tests.MedicoServiceApplication
{
    public class BuscaMedicoPorEspecialidadeCommandHandlerTests
    {
        private readonly Mock<IMedicoRepository> _medicoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BuscaMedicoPorEspecialidadeCommandHandler _handler;

        public BuscaMedicoPorEspecialidadeCommandHandlerTests()
        {
            _medicoRepositoryMock = new Mock<IMedicoRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new BuscaMedicoPorEspecialidadeCommandHandler(_medicoRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedMedicoDtos_WhenEspecialidadeExists()
        {
            // Arrange
            var especialidade = "Cardiologia";
            var request = new BuscaMedicoPorEspecialidadeCommandRequest { NomeEspecialidade = especialidade };

            var medicoEntities = new List<Medico> { new Medico { Id = 1, Nome = "Dr. João", Crm = "123", Senha = "abc123", ValorConsulta = 100 } };
            var medicoDtos = new List<MedicoDto> { new MedicoDto { Nome = "Dr. João", Crm = "123", ValorConsulta = 100 } };

            _medicoRepositoryMock
                .Setup(repo => repo.BuscaEspecialidade(especialidade))
                .ReturnsAsync(medicoEntities);

            _mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<MedicoDto>>(medicoEntities))
                .Returns(medicoDtos);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(medicoDtos, result);

            _medicoRepositoryMock.Verify(repo => repo.BuscaEspecialidade(especialidade), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<MedicoDto>>(medicoEntities), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoMedicosFound()
        {
            // Arrange
            var especialidade = "Neurologia";
            var request = new BuscaMedicoPorEspecialidadeCommandRequest { NomeEspecialidade = especialidade };

            _medicoRepositoryMock
                .Setup(repo => repo.BuscaEspecialidade(especialidade))
                .ReturnsAsync(new List<Medico>());

            _mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<MedicoDto>>(It.IsAny<IEnumerable<Medico>>()))
                .Returns(new List<MedicoDto>());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

            _medicoRepositoryMock.Verify(repo => repo.BuscaEspecialidade(especialidade), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<MedicoDto>>(It.IsAny<IEnumerable<Medico>>()), Times.Once);
        }
    }
}
