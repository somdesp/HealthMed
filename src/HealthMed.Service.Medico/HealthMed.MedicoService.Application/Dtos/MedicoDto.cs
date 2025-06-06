﻿namespace HealthMed.MedicoService.Application.Dtos
{
    public class MedicoDto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Crm { get; set; }
        public required double ValorConsulta { get; set; }
        public EspecialidadeDto? Especialidade { get; set; }
    }
}
