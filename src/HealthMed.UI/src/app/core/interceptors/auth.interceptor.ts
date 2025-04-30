import { HttpInterceptorFn } from '@angular/common/http';

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {
    const token = localStorage.getItem('token');
    const cloned = req.clone({
        setHeaders: {
            Authorization: token ? `Bearer ${token}` : ''
        }
    });

    return next(cloned);
};
