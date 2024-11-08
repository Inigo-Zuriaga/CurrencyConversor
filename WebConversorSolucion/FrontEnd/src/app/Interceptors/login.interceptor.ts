import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse} from '@angular/common/http';
import{Injectable} from '@angular/core';
import {AuthService} from '../services/auth.service';
import {catchError, Observable, throwError} from 'rxjs';
import {tap} from 'rxjs/operators';

// export const loginInterceptor: HttpInterceptorFn = (req, next) => {
//   return next(req);
// };
@Injectable()
export class loginInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(
        tap(event => {
          //console.log("entrando en el tap")
          if (event instanceof HttpResponse && req.url.endsWith('/login/')) {
            //console.log("entrando en el if")
            const accessToken = event.body.token;
            // console.log(event.body)
            this.authService.storeToken(accessToken);
            this.authService.logged.next(true);
          }
          // console.log("entrando en el else")
          // console.log(req.url)

        }),
        catchError((error) => {
          console.log("entrando en el catch")
          return throwError(() => error);
        })
      );
  }
}

