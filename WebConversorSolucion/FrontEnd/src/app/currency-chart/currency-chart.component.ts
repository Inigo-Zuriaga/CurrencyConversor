import { Component, OnInit } from '@angular/core';
import { ChartData } from 'chart.js';
import { ChartService } from '../services/chart.service';

@Component({
  selector: 'app-currency-chart',
  templateUrl: './currency-chart.component.html',
  styleUrls: ['./currency-chart.component.css']
})
export class CurrencyChartComponent implements OnInit {
  lineChartData: any = {
    labels: [],  // Aquí van las fechas
    datasets: [
      {
        data: [],  // Aquí van las tasas de cambio
        label: 'Exchange Rate',
        borderColor: '#00f',
        fill: false,
        tension: 0.1,
      },
    ],
  };

  constructor(private chartService: ChartService) {}

  ngOnInit() {
    this.loadExchangeHistory();
  }

  loadExchangeHistory() {
    const fromCurrency = 'USD';  // Ajusta según lo necesites
    const toCurrency = 'EUR';    // Ajusta según lo necesites

    this.chartService.getHistoricalData(fromCurrency, toCurrency).subscribe(
      (data) => {
        console.log('Data received for historical exchange rates:', data);

        // Asegúrate de que los datos recibidos tengan las propiedades "dates" y "rates"
        if (data && data.dates && data.rates) {
          // Asignamos los datos del gráfico
          this.lineChartData.labels = data.dates;  // Asignamos las fechas
          this.lineChartData.datasets[0].data = data.rates;  // Asignamos las tasas de cambio

          console.log('Chart data updated:', this.lineChartData);
        } else {
          console.error('Data format incorrect. Expected "dates" and "rates" arrays.');
        }
      },
      (error) => {
        console.error('Error loading exchange history', error);
      }
    );
  }
}
