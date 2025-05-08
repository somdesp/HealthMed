// header.component.ts
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

interface TokenPayload {
  unique_name: string;
  role: 'admin' | 'medico' | 'paciente';
  exp: number;
}

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  styleUrls: ['./header.component.scss'],
  templateUrl: './header.component.html'
}) export class HeaderComponent {


  private router = inject(Router);
  usuario: TokenPayload | null = null;
  public logo = ""

  constructor() {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        this.usuario = jwtDecode<TokenPayload>(token);
        if (this.usuario.role == 'medico')
          this.logo = "Consultas Disponiveis "
        else
          this.logo = "Central Agendamentos"

      } catch {
        this.logout();
      }
    }
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }

  avatarInicial(nome?: string): string {
    if (!nome) return '?';
    return nome.trim()[0].toUpperCase();
  }
}