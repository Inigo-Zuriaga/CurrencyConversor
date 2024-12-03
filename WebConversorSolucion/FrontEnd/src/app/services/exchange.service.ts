import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../environments/environment';
import { History } from '../Interfaces/ihistory';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExchangeService {


  // private apiUrl = 'https://appconversor-g3aagrgqg0hrejgh.northeurope-01.azurewebsites.net/api/api';

  private apiUrl = environment.apiUrl3;  // API/API
  private apiUrl2 = environment.apiUrl2; // API/History
  private apiUrl3 = environment.apiUrl4;  // API/Pdf

  // private apiUrl3 = 'https://appconversor-g3aagrgqg0hrejgh.northeurope-01.azurewebsites.net/api/History';
  // private apiUrl4 = 'https://appconversor-g3aagrgqg0hrejgh.northeurope-01.azurewebsites.net/api/Pdf';

  //Api de prueba
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

  createExchangeHistory(fromCoin:string,fromAmount:number,toCoin:string,toAmount:number,date:Date,
                        email:string): Observable<any> {

    const body = {
      fromCoin:fromCoin,
      fromAmount:fromAmount,
      toCoin:toCoin,
      toAmount:toAmount,
      date:date,
      email:email
    };
    return this.http.post(`${this.apiUrl2}/CreateHistory`,body);
  }
  createPdf(history:History[]): Observable<any> {

    return this.http.post(`${this.apiUrl3}`, history,{ responseType: 'blob' });
  }


  deleteHistory(historyId:number): Observable<any> {

    return this.http.post(`${this.apiUrl2}/DeleteHistory`, historyId);
  }

  // private apiUrl = 'http://localhost:45471/api/PruebaApi/exchange-data';
  pruebaConversor(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
}
