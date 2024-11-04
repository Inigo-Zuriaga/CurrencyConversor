import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExchangeService {

  constructor(private http: HttpClient) {

  }

  getExchangeRate(fromCurrency: string, toCurrency: string) {

    const apiUrl=`${environment.apiUrl}${environment.apiKey}/latest/USD`;
    this.http.get(apiUrl).subscribe((data: any) => {
      console.log(data);
    });
  }

  private apiUrl = 'http://localhost:45471/api/PruebaApi/exchange-data';
  pruebaConversor(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
}
