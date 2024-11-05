import { Component,OnInit } from '@angular/core';
import {ExchangeService} from '../../services/exchange.service';
import {IExchange} from '../../Interfaces/iexchange';

@Component({
  selector: 'app-conversor',
  templateUrl: './conversor.component.html',
  styleUrl: './conversor.component.css'
})
export class ConversorComponent {

  fromCurrency="";
  toCurrency="";
  exchangeRate: any;

  dataExchange: IExchange={
    result: "",
    base_code: "",
    target_code: "",
    conversion_rate: 0
  }

  data: any;

  constructor(private exchangeService: ExchangeService) {
  }

  getExchangeRate() {
    this.exchangeService.getExchangeRate(this.fromCurrency, this.toCurrency).subscribe(
      data => {
        this.exchangeRate = data;
        this.dataExchange.result = data.result;
        this.dataExchange.base_code = data.base_code;
        this.dataExchange.target_code = data.target_code;
        this.dataExchange.conversion_rate = data.conversion_rate;
      },
      error => {
        console.error('Error fetching exchange rate', error);
      }
    );
  }

  // ngOnInit() {
  //   this.exchangeService.getExchangeRate("a","a");
  //
  //   this.exchangeService.pruebaConversor().subscribe(
  //     (response) => {
  //       this.data = response;
  //     },
  //     (error) => {
  //       console.error('Error al obtener datos:', error);
  //     }
  // };
//   ngOnInit() {
//     this.exchangeService.pruebaConversor().subscribe(
//       (response: any) => {
//         this.data = response;
//       },
//       (error: any) => {
//         console.error('Error al obtener datos:', error);
//       }
//     );
//
// }
}

