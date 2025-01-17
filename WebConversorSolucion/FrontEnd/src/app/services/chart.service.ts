import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChartService {

  private apiUrl =environment.apiUrl3;  // /api/api

  constructor(private http: HttpClient) { }

  // Método para obtener los datos históricos de tasas de cambio
  getHistoricalData(fromCurrency: string, toCurrency: string): Observable<any> {
    const request = {
      FromCurrency: fromCurrency,
      ToCurrency: toCurrency,
    };

    return this.http.post(`${this.apiUrl}/historical-data`, request);
  }
}
