import { inject, Injectable } from '@angular/core';
import { CanActivate, CanActivateFn } from '@angular/router';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) { }

  canActivate(): boolean {
    if (this.authService.isAuthenticated()) {
      return true;
    }

    this.router.navigate(['/']);
    return false;
  }
}

export const authGuard: (tipoPermitido: string[]) => CanActivateFn =
  (tipoPermitido: string[]) => {
    return () => {
      const router = inject(Router);
      const token = localStorage.getItem('token');
      const tipo = localStorage.getItem('tipo');

      if (!token || !tipoPermitido.includes(tipo ?? '')) {
        router.navigate(['/login']);
        return false;
      }

      return true;
    };
  };