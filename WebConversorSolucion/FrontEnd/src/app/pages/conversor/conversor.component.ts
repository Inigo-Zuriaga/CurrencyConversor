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
  amount=0;
  exchangeRate: any;

  dataExchange: IExchange={
    result: "",
    base_code: "",
    target_code: "",
    conversion_rate: 0,
    conversion_result: 0
  }
  //En el constructor inyectamos los servicios que vayamos a usar.
  constructor(private exchangeService: ExchangeService) {}


  //Metodo el cual se encarga de hacer una llamada a la api mediante el metodo del servicio ExchangeService.
  //Asiganmos los datos recibidos a nuestra variable dataExchange.
  getExchange() {
    this.exchangeService.getExchangeRate(this.fromCurrency, this.toCurrency,this.amount).subscribe(
      data => {
        this.exchangeRate = data;
        this.dataExchange.result = data.result;
        this.dataExchange.base_code = data.base_code;
        this.dataExchange.target_code = data.target_code;
        this.dataExchange.conversion_rate = data.conversion_rate;
        this.dataExchange.conversion_result = data.conversion_result;
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

