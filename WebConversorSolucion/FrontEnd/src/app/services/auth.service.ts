import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import jwt_decode, {jwtDecode, JwtPayload} from 'jwt-decode';
import {environment} from "../environments/environment";

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  // private apiUrl = 'http://localhost:25850/api/user';  // URL de la API backend
  private apiUrl3 = 'http://localhost:25850/api/History';  // URL de la API backend
  private apiUrl=environment.apiUrl;
  private apiUrl2=environment.apiUrl2;
  constructor(private http: HttpClient) {}

  // método para iniciar sesión
  login(email: string, 
    password: string): Observable<any> {
    const body = 
    { 
      "email":email, 
      "password":password 
    };
    return this.http.post(`${this.apiUrl}/Login`, body);
  }

  // método para registrar un usuario nuevo
  signIn(name:string,
         lastName:string,
         email: string,
         password: string,
         fechaNacimiento:Date,
         img:string): Observable<any> {


    const body = { name,lastName,email,fechaNacimiento,password,img };
    return this.http.post(`${this.apiUrl}/SignIn`, body);

  }

  viewHistory(email: string): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const body = { email }; // Este es el formato correcto, un objeto JSON con la propiedad email
    console.log("El body");
    console.log(body);
    console.log(body.email.toString());
    return this.http.post(`${this.apiUrl3}/History`, JSON.stringify(email), { headers });
  }

  register(email: string, password: string, confirmPassword: string): Observable<any> {
    const body = { email, password, confirmPassword };
    return this.http.post(`${this.apiUrl}/SignIn`, body);
  }

  // variable que indica si el usuario está logueado o no
  logged :BehaviorSubject<boolean>=new BehaviorSubject<boolean>(this.UserIsLogged());

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

  // devuelve el nombre de usuario si el token está almacenado y es válido
  getUserName():string{

    const accessToken =this.getAccessToken();
    if(!accessToken){
     return '';
    }
    const decodedToken = this.decodeToken(accessToken);
    return decodedToken ? decodedToken.username:'';
}

}

