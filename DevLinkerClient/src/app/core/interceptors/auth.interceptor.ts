import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { EMPTY, Observable, catchError, finalize, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { LoaderService } from '../services/loader.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(
    private _router: Router,
    private _loaderService: LoaderService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    this._loaderService.isLoading.next(true);
    return next.handle(request).pipe(
      catchError(err => {
        if (err instanceof HttpErrorResponse) {
          switch (err.status) {
            case 401: this._router.navigateByUrl('account/login'); return EMPTY;
            case 404: this._router.navigateByUrl('not-found'); return EMPTY;
            default: return EMPTY;
          }
        }
        return throwError(() => err as unknown);
      }),
      finalize(() => {
        this._loaderService.isLoading.next(false);
      })
    );
  }
}
