import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ExchangeHistoryService {
  private apiUrl = 'http://localhost:25850/api/Api/historical-data'; // URL de tu API en el backend

  constructor(private http: HttpClient) {}

  // Método para obtener los datos históricos de la API
  getHistoricalData(fromCurrency: string, toCurrency: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}?fromCurrency=${fromCurrency}&toCurrency=${toCurrency}`);
  }
}
