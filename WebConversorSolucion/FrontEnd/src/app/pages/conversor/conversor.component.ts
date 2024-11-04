import { Component,OnInit } from '@angular/core';
import {ExchangeService} from '../../services/exchange.service';

@Component({
  selector: 'app-conversor',
  templateUrl: './conversor.component.html',
  styleUrl: './conversor.component.css'
})
export class ConversorComponent implements OnInit {

  data: any;

  constructor(private exchangeService: ExchangeService) {
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
  ngOnInit() {
    this.exchangeService.pruebaConversor().subscribe(
      (response: any) => {
        this.data = response;
      },
      (error: any) => {
        console.error('Error al obtener datos:', error);
      }
    );

}
}

