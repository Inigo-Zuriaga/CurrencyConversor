import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {HttpClient, HttpParams} from "@angular/common/http";
import jwt_decode, {jwtDecode, JwtPayload} from 'jwt-decode';


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/api/user';  // URL de la API backend

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    const body = { email, password, rememberMe: false };
    return this.http.post(`${this.apiUrl}/login`, body);
  }

  register(email: string, password: string, confirmPassword: string): Observable<any> {
    const body = { email, password, confirmPassword };
    return this.http.post(`${this.apiUrl}/register`, body);
  }


  logged :BehaviorSubject<boolean>=new BehaviorSubject<boolean>(this.UserIsLogged());
  constructor(private http: HttpClient) { }
  decodedToken:any;


  //Guarda el token en el localstorage
  storeToken(accesToken: string) {
  localStorage.setItem('accessToken', accesToken);
  }

  get isLogged(): Observable<boolean> {
    return this.logged.asObservable();
  }

  //Borra el token del localstorage
  deleteToken():void {
    localStorage.removeItem('accessToken');
  }
  getAccessToken():string{
    return <string>localStorage.getItem('accessToken');
  }
  getUserToken():string{
    return <string>localStorage.getItem('username');
  }

  DeleteUsername(): void {
    localStorage.removeItem('username');
  }
  //Comprueba si hay un token almacenado en el localstorage. Y devuelve true si lo hay.
  UserIsLogged():boolean{
    return !!localStorage.getItem('accessToken');
  }

  decodeToken(token:string):any{
    try {
      return jwtDecode(token);
    } catch (Error) {
      return null;
    }

  }
  getUserName():string{

    const accessToken =this.getAccessToken();
    if(!accessToken){
     return '';
    }
    const decodedToken = this.decodeToken(accessToken);
    return decodedToken ? decodedToken.username:'';
}

}

