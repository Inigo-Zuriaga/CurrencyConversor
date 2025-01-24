import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import {environment} from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChartService {

  private apiUrl = environment.apiUrl3; // /api/api

  constructor(private http: HttpClient) { }

  // Método para obtener los datos históricos de tasas de cambio
  getHistoricalData(fromCurrency: string, toCurrency: string): Observable<any> {
    const accessToken = localStorage.getItem('accessToken');

    if (!accessToken) {
      // Si no existe el accessToken, lanza un error.
      return throwError(() => new Error('Access token is required.'));
    }

    const request = {
      FromCurrency: fromCurrency,
      ToCurrency: toCurrency,
    };

    return this.http.post(`${this.apiUrl}/historical-data`, request, {
      headers: {
        Authorization: `Bearer ${accessToken}` // Incluye el token en la solicitud
      }
    });
  }
}
