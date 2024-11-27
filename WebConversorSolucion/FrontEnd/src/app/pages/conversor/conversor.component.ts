import {Component, OnInit,ChangeDetectorRef} from '@angular/core';
import coins from './coins.json';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ExchangeService } from '../../services/exchange.service';
import { AuthService } from '../../services/auth.service';
import { ChartService } from '../../services/chart.service';
import { Router } from '@angular/router';
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
  lastLength: number = 0;

    // Datos para el gráfico
    lineChartData: any = { datasets: [], labels: [] };
  historyData: any[] = [];
  constructor(
    private exchangeService: ExchangeService,
    private http: HttpClient,
    private authService: AuthService,
    private chartService: ChartService,
    private router: Router,

  ) {}






  ngOnInit() {
    this.filteredCurrencies = this.currencies;

    this.authService.historyData$.subscribe((data) => {
      this.historyData = data; // Cuando el historial cambia, lo actualizamos en el componente
    });

    // this.updateChartData();
    // this.getExchangeRate();
    this.email= this.authService.getUserEmail();
    this.authService.viewHistory(this.email).subscribe(
      (data) => {
        console.log("Datos recibidos:", data);
        this.historyData = data;

      },
      (error) => {
        console.error("Error al obtener el historial:", error);
      });
  }

  async getExchangeRate() {
    try {
      // Llamamos al servicio para obtener la tasa de cambio y esperamos la respuesta
      const data = await this.exchangeService.getExchangeRate(this.fromCurrency.shortname, this.toCurrency.shortname, this.amount).toPromise();

      console.log('Exchange rate fetched successfully', data);

      // Asumimos que `data` contiene el resultado de la conversión
      this.convertedAmount = data.conversion_result;

      // Recogemos el email del usuario logueado
      const fromCoin: string = this.fromCurrency.shortname;
      this.email = this.authService.getUserEmail();

      // Actualizamos los datos del gráfico si es necesario
      this.updateChartData();

      // Llamamos al servicio para crear el historial y esperar la respuesta
      await this.createExchangeHistory(fromCoin, data.conversion_result); // Esperamos la creación del historial

      // Actualizamos el historial en la vista
      this.updateHistory();

    } catch (error) {
      console.error('Error al obtener la tasa de cambio o crear el historial', error);
    }
  }

  async createExchangeHistory(fromCoin: string, conversionResult: number) {
    // Llamamos al servicio para crear el historial
    try {
      const response = await this.exchangeService.createExchangeHistory(
        fromCoin,
        this.amount,
        this.toCurrency.shortname,
        conversionResult,
        new Date(),
        this.email
      ).toPromise();

      console.log('Historial creado con éxito', response);
    } catch (error) {
      console.error('Error al crear el historial', error);
    }
  }

  // async onConversionSuccess() {
  //   try {
  //     const email = this.authService.getUserEmail();
  //
  //     // Simulamos obtener el historial actualizado y lo emitimos
  //     this.authService.fetchAndUpdateHistory(email); // Esto actualizará el BehaviorSubject
  //   } catch (error) {
  //     console.error('Error al obtener el historial:', error);
  //   }
  // }


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
  // getExchangeRate() {
  //   this.exchangeService.getExchangeRate(this.fromCurrency.shortname, this.toCurrency.shortname, this.amount).subscribe(
  //     (data) => {
  //       console.log('Exchange rate fetched successfully', data);
  //
  //       this.convertedAmount = data.conversion_result; // Asume que `data` tiene la estructura adecuada
  //
  //       // Recogemos el email del usuario logueado
  //       const fromCoin: string = this.fromCurrency.shortname;
  //       this.email = this.authService.getUserEmail();
  //
  //       this.updateChartData();
  //       // Llamamos al servicio para crear el historial
  //       this.exchangeService.createExchangeHistory(
  //         this.fromCurrency.shortname,
  //         this.amount,
  //         this.toCurrency.shortname,
  //         data.conversion_result,
  //         new Date(),
  //         this.email
  //
  //           ).subscribe(
  //             (response) => {
  //               console.log('Historial creado', response);
  //               this.updateHistory();
  //             },
  //             (error) => {
  //               console.error('Error al crear el historial', error);
  //             }
  //           );
  //     },
  //     (error: HttpErrorResponse) => {
  //       console.error('Error fetching exchange rate', error);
  //     }
  //   );
  // }

  updateHistory() {
    this.email = this.authService.getUserEmail();
    this.authService.viewHistory(this.email).subscribe(
      (data) => {
        console.log("Datos recibidos:", data);
        this.historyData = data;

      },
      (error) => {
        console.error("Error al obtener el historial:", error);
      });
  }

  chartReady: boolean = false;
  updateChartData() {
    //DESCOMENTAR PARA QUE FUNCIONE EL GRAFICO

    this.chartService.getHistoricalData(this.fromCurrency.shortname, this.toCurrency.shortname).subscribe(
      (data) => {
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
        this.chartReady = true;
        // Log para verificar
        console.log('Chart data updated:', this.lineChartData);
      },
      (error) => {
        console.error('Error fetching chart data', error);
        this.chartReady = false;
      }
    );
  }


  onAmountChange() {
    // Aquí puedes manejar los cambios en el input de cantidad si es necesario
  }

  protected readonly history = history;
}
