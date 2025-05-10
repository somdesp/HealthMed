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