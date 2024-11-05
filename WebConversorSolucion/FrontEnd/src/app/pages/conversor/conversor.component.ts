import { Component } from '@angular/core';
import { ExchangeService } from '../../services/exchange.service';
import { IExchange } from '../../Interfaces/iexchange';

@Component({
  selector: 'app-conversor',
  templateUrl: './conversor.component.html',
  styleUrls: ['./conversor.component.css']
})
export class ConversorComponent {
  fromCurrency = "USD"; // Moneda por defecto
  toCurrency = "EUR"; // Moneda por defecto
  amount = 0;
  dropdownOpenFrom = false;
  dropdownOpenTo = false;
  currencies = [
    { code: 'USD', symbol: '$' },
    { code: 'EUR', symbol: '€' },
    { code: 'GBP', symbol: '£' },
    // Agrega más monedas según sea necesario
  ];
  dataExchange: IExchange = {
    result: "",
    base_code: "",
    target_code: "",
    conversion_rate: 0,
    conversion_result: 0
  };

  constructor(private exchangeService: ExchangeService) {}

  toggleDropdown(type: string) {
    if (type === 'from') {
      this.dropdownOpenFrom = !this.dropdownOpenFrom;
      this.dropdownOpenTo = false; // Cerrar el otro dropdown si está abierto
    } else {
      this.dropdownOpenTo = !this.dropdownOpenTo;
      this.dropdownOpenFrom = false; // Cerrar el otro dropdown si está abierto
    }
  }

  selectCurrency(currency: any, type: string) {
    if (type === 'from') {
      this.fromCurrency = currency.code;
      this.dropdownOpenFrom = false;
    } else {
      this.toCurrency = currency.code;
      this.dropdownOpenTo = false;
    }
  }

  get fromCurrencySymbol() {
    const currency = this.currencies.find(c => c.code === this.fromCurrency);
    return currency ? currency.symbol : '';
  }

  get toCurrencySymbol() {
    const currency = this.currencies.find(c => c.code === this.toCurrency);
    return currency ? currency.symbol : '';
  }

  getExchangeRate() {
    this.exchangeService.getExchangeRate(this.fromCurrency, this.toCurrency, this.amount).subscribe(
      data => {
        this.dataExchange = data; // Asume que data tiene la estructura adecuada
      },
      error => {
        console.error('Error fetching exchange rate', error);
      }
    );
  }

  onAmountChange() {
    // Aquí puedes manejar los cambios en el input de cantidad si es necesario
  }
}
