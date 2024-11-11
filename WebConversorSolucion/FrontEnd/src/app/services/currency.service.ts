import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


//Cambiar : Crear una interfaz Currency en un archivo a parte con los campos id, name, shortname y symbol
interface Currency {
  id: number;
  name: string;
  shortname: string;
  symbol: string;
}

@Injectable({
  providedIn: 'root',
})
export class CurrencyService {
  private readonly jsonUrl = './pages/conversor/coins.json';

  constructor(private http: HttpClient) {}

  getCurrencies(): Observable<Currency[]> {
    return this.http.get<Currency[]>(this.jsonUrl);
  }
}
