import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
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
    private route: Router,

  ) {}

  ngOnInit() {

    if (!this.authService.UserIsLogged()){
      this.route.navigate(['/']);
    }

    this.filteredCurrencies = this.currencies;

    // Suscribirse a los cambios en el historial del usuario
    this.authService.historyData$.subscribe((data) => {
      this.historyData = data; // Actualizar el historial cuando cambie
    });

    // Obtener el correo del usuario autenticado
    this.email = this.authService.getUserEmail();

    // Obtener el historial de conversiones del usuario
    this.authService.viewHistory(this.email).subscribe(
      (data) => {
        console.log("Datos recibidos:", data);
        this.historyData = data;
      },
      (error) => {
        console.error("Error al obtener el historial:", error);
      }
    );
  }

  swapCurrencies() {
    // Intercambiar las monedas de origen y destino
    const temp = this.fromCurrency;
    this.fromCurrency = this.toCurrency;
    this.toCurrency = temp;
  }

  async getExchangeRate() {
    try {
      // Obtener la tasa de cambio usando el servicio ExchangeService
      const data = await this.exchangeService.getExchangeRate(
        this.fromCurrency.shortname,
        this.toCurrency.shortname,
        this.amount
      ).toPromise();

      console.log('Tasa de cambio obtenida correctamente', data);

      // Guardar el monto convertido
      this.convertedAmount = data.conversion_result;

      // Crear el historial de la conversión
      const fromCoin: string = this.fromCurrency.shortname;
      await this.createExchangeHistory(fromCoin, data.conversion_result);

      // Actualizar el historial y los datos del gráfico
      this.updateHistory();
      this.updateChartData();
    } catch (error) {
      console.error('Error al obtener la tasa de cambio o crear el historial', error);
    }
  }

  async createExchangeHistory(fromCoin: string, conversionResult: number) {
    // Crear el historial de conversión
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

  toggleDropdown(select: 'from' | 'to') {
    // Alternar la visibilidad de los dropdowns
    if (select === 'from') {
      this.dropdownOpenFrom = !this.dropdownOpenFrom;
      this.dropdownOpenTo = false;
    } else {
      this.dropdownOpenTo = !this.dropdownOpenTo;
      this.dropdownOpenFrom = false;
    }
  }

  selectCurrency(currency: Currency, type: 'from' | 'to') {
    // Seleccionar una moneda para la conversión
    if (type === 'from') {
      this.fromCurrency = currency;
    } else {
      this.toCurrency = currency;
    }
    this.closeDropdowns();
  }

  closeDropdowns() {
    // Cerrar ambos dropdowns
    this.dropdownOpenFrom = false;
    this.dropdownOpenTo = false;
  }

  filterCurrencies(event: Event) {
    // Filtrar las monedas según el texto ingresado
    const searchTerm = (event.target as HTMLInputElement).value.toLowerCase();
    this.filteredCurrencies = this.currencies.filter(
      (currency) =>
        currency.shortname.toLowerCase().includes(searchTerm) ||
        currency.symbol.toLowerCase().includes(searchTerm) ||
        currency.name.toLowerCase().includes(searchTerm)
    );
  }

  updateHistory() {
    // Actualizar el historial de conversiones
    this.email = this.authService.getUserEmail();
    this.authService.viewHistory(this.email).subscribe(
      (data) => {
        console.log("Datos recibidos:", data);
        this.historyData = data;
      },
      (error) => {
        console.error("Error al obtener el historial:", error);
      }
    );
  }

  chartReady: boolean = false;
  
  updateChartData() {
    // Actualizar los datos del gráfico con los datos históricos
    this.chartService.getHistoricalData(this.fromCurrency.shortname, this.toCurrency.shortname).subscribe(
      (data) => {
        console.log('Datos recibidos:', data);

        const timeSeries = data['Time Series FX (Daily)'];
        if (!timeSeries) {
          console.error('Faltan datos de la serie temporal en la respuesta.');
          return;
        }

        // Procesar las fechas y valores de cierre
        const labels = Object.keys(timeSeries).reverse(); // Fechas en orden ascendente
        const values = labels.map(date => {
          const closeValue = timeSeries[date]?.['4. close'];
          return closeValue ? parseFloat(closeValue) : 0; // Asegurarse de que sea un número
        });

        console.log('Fechas procesadas:', labels);
        console.log('Valores procesados:', values);

        // Asignar los datos al gráfico
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
        console.log('Datos del gráfico actualizados:', this.lineChartData);
      },
      (error) => {
        console.error('Error al obtener los datos del gráfico', error);
        this.chartReady = false;
      }
    );
  }

  onAmountChange() {
    // Aquí puedes manejar los cambios en el monto si es necesario
  }

  protected readonly history = history;
}
