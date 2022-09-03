import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpResponse
} from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable, throwError } from 'rxjs';

import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private auth: AuthService, private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    request = request.clone({
      withCredentials: true
    });

    return next.handle(request).pipe<HttpEvent<any>>(
    
      catchError((err: any, caught: Observable<HttpEvent<any>>)=> {
        
        // if (err.status === 401) {

        //   // return this.handle401Error(request, next);

        //   this.auth.logout();

        //   this.router.navigate(['/login'])

        // }

        // if(err.status === 403){

        //     if( this.auth.isLoggedIn() ){
        //       this.router.navigate(['/panel'])
        //     }else{
        //       this.router.navigate(['/login'])
        //     }

        // }

        return throwError(err);
      
      })

      
    );

  }

}