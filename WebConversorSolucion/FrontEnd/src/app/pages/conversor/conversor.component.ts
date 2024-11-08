import { Component, OnInit } from '@angular/core';
import coins from './coins.json';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ExchangeService } from '../../services/exchange.service';// Asegúrate de que el servicio esté importado correctamente
interface Currency {
  name: string;
  shortname: string;
  symbol: string;
}

@Component({
  selector: 'app-conversor',
  templateUrl: './conversor.component.html',
  styleUrls: ['./conversor.component.css'],
})
export class ConversorComponent implements OnInit {
  amount: number = 0;
  convertedAmount: number = 0;
  fromCurrency: Currency = { name: 'United States Dollar', shortname: 'USD', symbol: '$' };
  toCurrency: Currency = { name: 'Euro', shortname: 'EUR', symbol: '€' };
  currencies: Currency[] = coins;
  dropdownOpenFrom: boolean = false;
  dropdownOpenTo: boolean = false;
  filteredCurrencies: Currency[] = [];

  constructor(private exchangeService: ExchangeService, private http: HttpClient) {}

  ngOnInit() {
    this.filteredCurrencies = this.currencies;
  }

  toggleDropdown(select: 'from' | 'to') {
    this.dropdownOpenFrom = select === 'from' ? !this.dropdownOpenFrom : false;
    this.dropdownOpenTo = select === 'to' ? !this.dropdownOpenTo : false;
  }

  selectCurrency(currency: Currency, type: 'from' | 'to') {
    if (type === 'from') {
      this.fromCurrency = currency;
    } else {
      this.toCurrency = currency;
    }
    this.closeDropdowns();
  }

  closeDropdowns() {
    this.dropdownOpenFrom = false;
    this.dropdownOpenTo = false;
  }

  filterCurrencies(event: Event) {
    const searchTerm = (event.target as HTMLInputElement).value.toLowerCase();
    this.filteredCurrencies = this.currencies.filter(
      (currency) =>
        currency.shortname.toLowerCase().includes(searchTerm) ||
        currency.symbol.toLowerCase().includes(searchTerm) ||
        currency.name.toLowerCase().includes(searchTerm)
    );
  }

  // Actualización del método para llamar al servicio
  getExchangeRate() {
    this.exchangeService.getExchangeRate(this.fromCurrency.shortname, this.toCurrency.shortname, this.amount).subscribe(
      (data) => {
        this.convertedAmount = data.conversion_result; // Asume que `data` tiene la estructura adecuada
      },
      (error: HttpErrorResponse) => {
        console.error('Error fetching exchange rate', error);
      }
    );
  }
//***************************************
  user="usuario";
  pass="contraseña";
  getExchangeRate2() {
    this.exchangeService.getExchangeRate2(this.user, this.pass).subscribe(

      data => {
        console.log("Usuario: "+this.user+" Contraseña: "+this.pass);
        console.log("Entraaaa")
        console.log('Login successful', data);
        // this.dataExchange = data; // Asume que data tiene la estructura adecuada
      },
      error => {
        console.log("Usuario: "+this.user+" Contraseña: "+this.pass);
        console.error('Error fetching exchange rate', error);
      }
    );
  }
////*****************************
  onAmountChange() {
    // Aquí puedes manejar los cambios en el input de cantidad si es necesario
  }
}
