export interface Medico {
    valorConsulta: number;
    id: number;
    nome: string;
    especialidade: Especialidade;
    crm: string;
}

export interface Especialidade {
    nome: string
}

export interface ConsultasMarcadasMedico {
    id: number
    dataHora: string
    pacienteId: number
    status: string
    agendaId: number
    paciente: string
}

export interface ConsultasMarcadasPaciente {
    id: number
    dataHora: string
    agendaId: number
    medico: string
    valorConsulta: number
    especialidade: string
    status: string
}
