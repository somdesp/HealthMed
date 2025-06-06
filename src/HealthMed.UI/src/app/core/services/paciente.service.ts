import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";

@Injectable({ providedIn: 'root' })
export class PacienteService {
    private http = inject(HttpClient);

    BuscaMedicos(especialidade: string) {
        let apiUrl = `${environment.apiGateway}/BuscaEspecialidade`;
        return this.http.post(apiUrl, { nomeEspecialidade: especialidade });
    }

}
