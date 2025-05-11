namespace HealthMed.BuildingBlocks.Contracts.Responses;

public record PacientesResponse(IEnumerable<PacienteResponse> PacienteResponses);
public record PacienteResponse(int Id, string Nome);
