import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  constructor(private router: Router) { }

  irParaLogin(tipo: 'paciente' | 'medico') {
    this.router.navigate([`/login/${tipo}`]);
  }
}
