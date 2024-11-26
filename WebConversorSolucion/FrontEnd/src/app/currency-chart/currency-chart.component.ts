import {Component, Input, OnChanges, SimpleChanges, ChangeDetectorRef, OnInit} from '@angular/core';
import { ChartData } from 'chart.js';
import { ChartService } from '../services/chart.service';
@Component({
  selector: 'app-currency-chart',
  templateUrl: './currency-chart.component.html',
  styleUrls: ['./currency-chart.component.css']
})
export class CurrencyChartComponent implements OnInit, OnChanges {
  @Input() lineChartData: any;
  // lineChartData: any = {
  //   labels: [],  // Aquí van las fechas
  //   datasets: [
  //     {
  //       data: [],  // Aquí van las tasas de cambio
  //       label: 'Exchange Rate',
  //       borderColor: '#00f',
  //       fill: false,
  //       tension: 0.1,
  //     },
  //   ],
  // };

  constructor(private chartService: ChartService,private cdRef: ChangeDetectorRef) {}

  ngOnInit() {
    console.log('Initial chart data:', this.lineChartData);
  }
  // ngOnChanges(changes: SimpleChanges) {
  //   if (changes['lineChartData']) {
  //     console.log('Chart data updated:', this.lineChartData);
  //   }
  // }
  ngOnChanges(changes: SimpleChanges) {
    if (changes['lineChartData']) {
      console.log('Chart data has changed:', this.lineChartData);
      // Forzar la detección de cambios
      this.cdRef.detectChanges();
    }
  }

  // ngOnInit() {
  //   this.loadExchangeHistory();
  // }

  loadExchangeHistory() {
    const fromCurrency = 'USD';  // Ajusta según lo necesites
    const toCurrency = 'EUR';    // Ajusta según lo necesites

    this.chartService.getHistoricalData(fromCurrency, toCurrency).subscribe(
      (data) => {
        console.log('Data received for historical exchange rates:', data);

        const timeSeries = data['Time Series FX (Daily)']; // Asegúrate de usar la clave correcta
        if (timeSeries) {
          // Extraer fechas y valores de cierre
          const dates = Object.keys(timeSeries).reverse(); // Fechas en orden ascendente
          const rates = dates.map(date => parseFloat(timeSeries[date]['4. close'])); // Valores de cierre

          console.log('Processed Dates:', dates);
          console.log('Processed Rates:', rates);

          // Configurar los datos para el gráfico
          this.lineChartData = {
            labels: dates, // Fechas
            datasets: [
              {
                label: `Exchange Rate (${fromCurrency} to ${toCurrency})`,
                data: rates, // Valores de cierre
                borderColor: '#00f',
                backgroundColor: 'rgba(0, 0, 255, 0.2)',
                fill: true,
                tension: 0.1,
              },
            ],
          };

          console.log('Chart data prepared:', this.lineChartData);

          // Forzar la detección de cambios
          // this.cdRef.detectChanges();
        } else {
          console.error('Time Series data is missing in the response.');
        }
      },
      (error) => {
        console.error('Error loading exchange history', error);
      }
    );
  }

}
