import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

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

  logout(): Observable<any> {
    return this.http.post(`${this.apiUrl}/logout`, {});
  }
}

