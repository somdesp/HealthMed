import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";

@Injectable({ providedIn: 'root' })
export class MedicoService {

    private http = inject(HttpClient);

    MeusAgendamentos() {
        let apiUrl = `${environment.apiGateway}/medico/MeusAgendamentos`;
        return this.http.get(apiUrl);
    }

    getAgendas() {
        let apiUrl = `${environment.apiGateway}/medico/BuscaMinhasAgendas`;
        return this.http.get(apiUrl);
    }
    deletaAgenda(id: number) {
        let apiUrl = `${environment.apiGateway}/medico/DeletaAgenda/${id}`;
        return this.http.delete(apiUrl);
    }

    addAgendas(NovaAgenda: any) {
        let apiUrl = `${environment.apiGateway}/medico/NovaAgenda`;
        return this.http.post(apiUrl, {
            "DataHora": NovaAgenda
        });
    }


}
