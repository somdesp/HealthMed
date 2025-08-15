import { HttpInterceptorFn } from "@angular/common/http";
import { inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {
    const token = localStorage.getItem('token');

    // Clonando a requisição para adicionar o cabeçalho de autorização
    const cloned = req.clone({
        setHeaders: {
            Authorization: token ? `Bearer ${token}` : ''
        }
    });

    // Passando a requisição clonada adiante
    return next(cloned).pipe();
};
