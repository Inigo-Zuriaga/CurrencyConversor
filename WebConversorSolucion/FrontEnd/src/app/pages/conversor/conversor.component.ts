import { Component, OnInit } from '@angular/core';
import coins from './coins.json';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ExchangeService } from '../../services/exchange.service';
import { AuthService } from '../../services/auth.service';
import { ChartService } from '../../services/chart.service';

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
  email: string = '';


    // Datos para el gráfico
    lineChartData: any = { datasets: [], labels: [] };

  constructor(
    private exchangeService: ExchangeService,
    private http: HttpClient,
    private authService: AuthService,
    private chartService: ChartService
  ) {}

  ngOnInit() {
    this.filteredCurrencies = this.currencies;
    // this.updateChartData();
    // this.getExchangeRate();
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

  // Método para obtener la tasa de cambio
  getExchangeRate() {
    this.exchangeService.getExchangeRate(this.fromCurrency.shortname, this.toCurrency.shortname, this.amount).subscribe(
      (data) => {
        console.log('Exchange rate fetched successfully', data);

        this.convertedAmount = data.conversion_result; // Asume que `data` tiene la estructura adecuada

        // Recogemos el email del usuario logueado
        const fromCoin: string = this.fromCurrency.shortname;
        this.email = this.authService.getUserEmail();

        this.updateChartData();
        // Llamamos al servicio para crear el historial
        this.exchangeService.createExchangeHistory(
          this.fromCurrency.shortname,
          this.amount,
          this.toCurrency.shortname,
          data.conversion_result,
          new Date(),
          this.email

            ).subscribe(
              (response) => {
                console.log('Historial creado', response);
              },
              (error) => {
                console.error('Error al crear el historial', error);
              }
            );
      },
      (error: HttpErrorResponse) => {
        console.error('Error fetching exchange rate', error);
      }
    );
  }

  updateChartData() {
    this.chartService.getHistoricalData(this.fromCurrency.shortname, this.toCurrency.shortname).subscribe(
      (data) => {
      //   console.log('Raw data received:', data);
      //
      //   const labels = Object.keys(data).reverse(); // Fechas
      //   const values = labels.map((date) => {
      //     const value = parseFloat(data[date]?.['4. close']);
      //     console.log(`Date: ${date}, Value: ${value}`); // Verifica cada valor
      //     return value;
      //   });
      //
      //   console.log('Processed labels:', labels);
      //   console.log('Processed values:', values);
      //
      //   this.lineChartData = {
      //     labels,
      //     datasets: [
      //       {
      //         data: values,
      //         label: `${this.fromCurrency.shortname} to ${this.toCurrency.shortname}`,
      //         borderColor: '#3e95cd',
      //         fill: false,
      //       },
      //     ],
      //   };
      // },
        console.log('Raw data received:', data);

        const timeSeries = data['Time Series FX (Daily)'];
        if (!timeSeries) {
          console.error('Time Series data is missing in the response.');
          return;
        }

        // Procesa las fechas y valores de cierre
        const labels = Object.keys(timeSeries).reverse(); // Fechas en orden ascendente
        const values = labels.map(date => {
          const closeValue = timeSeries[date]?.['4. close'];
          return closeValue ? parseFloat(closeValue) : 0; // Asegúrate de que sea un número
        });

        console.log('Processed labels:', labels);
        console.log('Processed values:', values);

        // Asigna los datos al gráfico
        this.lineChartData = {
          labels, // Fechas
          datasets: [
            {
              data: values, // Valores de cierre
              label: `${this.fromCurrency.shortname} to ${this.toCurrency.shortname}`,
              borderColor: '#3e95cd',
              fill: false,
            },
          ],
        };

        // Log para verificar
        console.log('Chart data updated:', this.lineChartData);
      },
      (error) => {
        console.error('Error fetching chart data', error);
      }
    );
  }


  onAmountChange() {
    // Aquí puedes manejar los cambios en el input de cantidad si es necesario
  }

}
