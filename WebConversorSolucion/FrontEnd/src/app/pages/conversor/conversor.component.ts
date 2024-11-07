import { Component, OnInit } from '@angular/core';
import coins from './coins.json';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

// Definición de la interfaz de moneda
interface Currency {
  id: number;
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
  amount: number = 0; // Cantidad a convertir
  convertedAmount: number = 0; // Resultado de la conversión
  fromCurrency: Currency = { id: 1, name: 'United States Dollar', shortname: 'USD', symbol: '$' }; // Moneda origen
  toCurrency: Currency = { id: 2, name: 'Euro', shortname: 'EUR', symbol: '€' }; // Moneda destino
  currencies: Currency[] = coins; // Lista completa de monedas del JSON
  dropdownOpenFrom: boolean = false; // Controla el desplegable del selector origen
  dropdownOpenTo: boolean = false; // Controla el desplegable del selector destino
  filteredCurrencies: Currency[] = []; // Lista de monedas filtrada para el buscador

  constructor(private http: HttpClient) {}

  ngOnInit() {
    // Inicializa el listado filtrado con todas las monedas
    this.filteredCurrencies = this.currencies;
  }

  // Función para alternar el dropdown de monedas
  toggleDropdown(select: 'from' | 'to') {
    this.dropdownOpenFrom = select === 'from' ? !this.dropdownOpenFrom : false;
    this.dropdownOpenTo = select === 'to' ? !this.dropdownOpenTo : false;
  }

  // Selecciona una moneda y cierra el desplegable
  selectCurrency(currency: Currency, type: 'from' | 'to') {
    if (type === 'from') {
      this.fromCurrency = currency;
    } else {
      this.toCurrency = currency;
    }
    this.closeDropdowns();
  }

  // Cierra ambos desplegables
  closeDropdowns() {
    this.dropdownOpenFrom = false;
    this.dropdownOpenTo = false;
  }

  // Filtrar monedas basado en el término de búsqueda (por código o nombre)
  filterCurrencies(event: Event) {
    const searchTerm = (event.target as HTMLInputElement).value.toLowerCase();
    this.filteredCurrencies = this.currencies.filter(
      (currency) =>
        currency.shortname.toLowerCase().includes(searchTerm) ||
        currency.symbol.toLowerCase().includes(searchTerm) ||
        currency.name.toLowerCase().includes(searchTerm) // Buscamos por nombre, shortname y símbolo
    );
  }

  // Realiza la solicitud para obtener la tasa de cambio
  getExchangeRate() {
    const url = 'http://localhost:25850/api/api/exchange-rate';
    const requestBody = {
      fromCurrency: this.fromCurrency.shortname,
      toCurrency: this.toCurrency.shortname,
      amount: this.amount,
    };

    this.http.post<any>(url, requestBody).subscribe(
      (response) => {
        this.convertedAmount = response.conversion_result;
      },
      (error: HttpErrorResponse) => {
        console.error('Error fetching exchange rate', error);
      }
    );
  }
}
