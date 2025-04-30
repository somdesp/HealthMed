import { Component } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-paciente-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './paciente-login.component.html',
  styleUrl: './paciente-login.component.scss'
})
export class PacienteLoginComponent {
  cpf = '';
  senha = '';
  errorMessage = '';
  loading = false;
  loginError = false;

  constructor(private authService: AuthService, private router: Router) { }
  login() {
    this.authService.loginPaciente(this.cpf, this.senha).subscribe({
      next: () => {
        this.loginError = false;
        this.router.navigate(['/paciente']);
      },
      error: () => {
        this.loginError = true;
      }
    });
  }

  isValidCPF(crm_cpf: string): boolean {
    return /^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$/.test(crm_cpf); // Aceita com ou sem pontuação
  }
}
