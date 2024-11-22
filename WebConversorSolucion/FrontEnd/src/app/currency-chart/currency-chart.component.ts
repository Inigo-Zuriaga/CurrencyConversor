import { Component, OnInit } from '@angular/core';
import { ExchangeHistoryService } from '../services/chart.service';
import { ChartData } from 'chart.js'; // Importa ChartData de chart.js para tipar los datos

@Component({
  selector: 'app-currency-chart',
  templateUrl: './currency-chart.component.html',
  styleUrls: ['./currency-chart.component.css'],
})
export class CurrencyChartComponent implements OnInit {
  //
  // lineChartData: ChartData<'line'> = {
  //   labels: [],
  //   datasets: [
  //     {
  //       data: [],
  //       label: 'Historical Data',
  //       borderColor: '#42A5F5',
  //       fill: false,
  //     },
  //   ],
  // };

  fromCurrency: string = 'USD'; // Moneda de origen por defecto
  toCurrency: string = 'EUR'; // Moneda de destino por defecto

  constructor(private exchangeHistoryService: ExchangeHistoryService) {}

  ngOnInit(): void {
    this.loadHistoricalData(); // Cargar datos históricos al iniciar
  }

  loadHistoricalData(): void {
    this.exchangeHistoryService.getHistoricalData(this.fromCurrency, this.toCurrency).subscribe(
      (data) => {
        console.log('Datos históricos:', data);
        this.prepareChartData(data);
      },
      (error) => {
        console.error('Error al cargar los datos históricos:', error);
      }
    );
  }

  // Preparar los datos para el gráfico
  prepareChartData(data: any): void {
    // Suponiendo que los datos de la API contienen la estructura adecuada
    const dates = Object.keys(data['Time Series FX (Daily)']);
    const values = Object.values(data['Time Series FX (Daily)']).map((item: any) => item['4. close']);

    //this.lineChartData.labels = dates; // Fechas para el eje X
   // this.lineChartData.datasets[0].data = values; // Valores de conversión para el eje Y


  }
}
