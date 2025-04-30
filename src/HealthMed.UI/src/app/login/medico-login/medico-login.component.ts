import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-medico-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './medico-login.component.html',
  styleUrl: './medico-login.component.scss'
})
export class MedicoLoginComponent {
  crm = '';
  senha = '';
  errorMessage = '';
  loading = false;
  loginError = false;

  constructor(private authService: AuthService, private router: Router) { }
  login() {
    this.authService.loginMedico(this.crm, this.senha).subscribe({
      next: () => {
        this.loginError = false;
        this.router.navigate(['/medico']);
      },
      error: () => {
        this.loginError = true;
      }
    });
  }

  isValidCRM(crm_cpf: string): boolean {
    return /^\d{4,}$/.test(crm_cpf); // Apenas números, mínimo 4 dígitos
  }

}
