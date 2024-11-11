import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChartService {
  private apiKey = '5092QPE29Y58ZS7V';
  private apiUrl = 'https://www.alphavantage.co/query';

  constructor(private http: HttpClient) {}

  getMonthlyHistory(symbol: string): Observable<any> {
    const params = {
      function: 'FX_MONTHLY',
      from_symbol: symbol,
      to_symbol: 'USD', // Puedes cambiarlo a la divisa deseada
      apikey: this.apiKey
    };
    return this.http.get(this.apiUrl, { params });
  }
}
