import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { catchError, finalize } from 'rxjs/operators';

@Injectable()
export class SpinnerInterceptor implements HttpInterceptor {

    constructor(private spinner: NgxSpinnerService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Inicia o spinner antes da requisição
        this.spinner.show();

        return next.handle(req).pipe(
            finalize(() => {
                // Finaliza o spinner após a requisição (independente de sucesso ou erro)
                this.spinner.hide();
            }),
            catchError((error) => {
                // Em caso de erro, podemos tratar de alguma forma (se necessário)
                this.spinner.hide(); // Esconde o spinner caso ocorra erro
                throw error; // Re-levanta o erro
            })
        );
    }
}
