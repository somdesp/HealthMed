import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { jwtDecode } from "jwt-decode";
import { UserLogged } from '../model/currentUser';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {

  private http = inject(HttpClient);
  private router = inject(Router);

  loginMedico(crm: string, senha: string): Observable<any> {
    let apiUrl = `${environment.apiGateway}/Medico/Login`;
    return this.http.post<{ accessToken: string }>(apiUrl, {
      crm,
      senha
    }).pipe(
      tap(response => {
        localStorage.setItem('token', response.accessToken);
      })
    );
  }

  loginPaciente(cpf: string, senha: string): Observable<any> {
    let apiUrl = `${environment.apiGateway}/Paciente/Login`;

    return this.http.post<{ accessToken: string }>(apiUrl, {
      cpf,
      senha
    }).pipe(
      tap(response => {
        localStorage.setItem('token', response.accessToken);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  public GetJwtAcess(): any {
    let CurrentUser = localStorage.getItem('token');
    if (CurrentUser == null) {
      return "";
    }

    let tokenPayload = jwtDecode<UserLogged>(CurrentUser);
    if (tokenPayload.exp < Math.floor(Date.now() / 1000))
      return "";

    return tokenPayload.role;
  }
}
