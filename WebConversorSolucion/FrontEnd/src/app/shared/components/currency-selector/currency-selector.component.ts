import { Component, EventEmitter, Output } from '@angular/core';

interface Currency {
  symbol: string;
  code: string;
}

@Component({
  selector: 'app-currency-selector',
  templateUrl: './currency-selector.component.html',
  styleUrls: ['./currency-selector.component.css'],
})
export class CurrencySelectorComponent {
  @Output() currencyChange = new EventEmitter<string>();
  @Output() amountChange = new EventEmitter<number>();

  amount: number = 0;
  dropdownOpen: boolean = false;
  selectedCurrency: Currency = { symbol: '$', code: 'USD' };

  currencies: Currency[] = [
    { symbol: '$', code: 'USD' },
    { symbol: '€', code: 'EUR' },
    { symbol: '¥', code: 'JPY' },
    { symbol: 'kr', code: 'DKK' },
    { symbol: '£', code: 'GBP' },
  ];

  toggleDropdown() {
    this.dropdownOpen = !this.dropdownOpen;
  }

  selectCurrency(currency: Currency) {
    this.selectedCurrency = currency;
    this.dropdownOpen = false;
    this.currencyChange.emit(this.selectedCurrency.code); // Emitimos el código de la moneda seleccionada
  }

  onAmountChange() {
    this.amountChange.emit(this.amount); // Emitimos el valor del input
  }
}
