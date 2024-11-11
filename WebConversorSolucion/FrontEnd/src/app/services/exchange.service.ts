import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

//import { environment } from '../environments/environment';

import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExchangeService {


  private apiUrl = 'http://localhost:25850/api/api'

  //Api de prueba
  private apiUrl2 = 'http://localhost:5299/api/Auth';
  constructor(private http: HttpClient) {}

  // getExchangeRate(fromCurrency: string, toCurrency: string) {
  //
  //   const apiUrl=`${environment.apiUrl}${environment.apiKey}/latest/USD`;
  //   this.http.get(apiUrl).subscribe((data: any) => {
  //     console.log(data);
  //   });
  // }

  getExchangeRate(fromCurrency: string, toCurrency: string,amount:number): Observable<any> {
    const body = {
      fromCurrency:fromCurrency,
      toCurrency:toCurrency,
      Amount:amount
    };

    return this.http.post(`${this.apiUrl}/exchange-rate`, body);
  }
  getExchangeRate2(username: string, password: string): Observable<any> {
    const headers = { 'Content-Type': 'application/json' };
    const body = {
      email:username,
      password:password
    };

    return this.http.post(`${this.apiUrl2}/login`, body, { headers });
  }


  // private apiUrl = 'http://localhost:45471/api/PruebaApi/exchange-data';
  pruebaConversor(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
}
