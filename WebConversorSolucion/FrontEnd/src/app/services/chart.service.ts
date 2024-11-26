import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChartService {

  private apiUrl = 'http://localhost:25850/api/api';  // URL de tu API

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
