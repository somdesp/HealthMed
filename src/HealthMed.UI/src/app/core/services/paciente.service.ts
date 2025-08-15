import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";

@Injectable({ providedIn: 'root' })
export class PacienteService {

    private http = inject(HttpClient);

    BuscaMedicos(especialidade: string) {
        let apiUrl = `${environment.apiGateway}/Paciente/BuscaMedicoPorEspecialidade/${especialidade}`;
        return this.http.get(apiUrl);
    }

    BuscaConsultasMarcadas() {
        let apiUrl = `${environment.apiGateway}/paciente/MeusAgendamentos`;
        return this.http.get(apiUrl);
    }

    AgendasMedico(medicoId: number) {
        let apiUrl = `${environment.apiGateway}/Paciente/BuscaAgendaPorMedicoId/${medicoId}`;
        return this.http.get(apiUrl);
    }


}
