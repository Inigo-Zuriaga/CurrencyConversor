// src/app/components/currency-chart/currency-chart.component.ts
import { Component, OnInit } from '@angular/core';
import { ChartData } from 'chart.js';
import { ChartService } from '../services/chart.service';

@Component({
  selector: 'app-currency-chart',
  templateUrl: './currency-chart.component.html',
  styleUrls: ['./currency-chart.component.css']
})
export class CurrencyChartComponent implements OnInit {
  public lineChartData: ChartData<'line'> = {
    labels: [], // Fechas
    datasets: [
      {
        label: 'EUR to USD Monthly Evolution',
        data: [], // Datos de precios
        fill: false,
        borderColor: '#93032E',
        tension: 0.1
      }
    ]
  };

  constructor(private currencyService: ChartService) {}

  ngOnInit(): void {
    this.loadCurrencyData();
  }

  private loadCurrencyData(): void {
    this.currencyService.getMonthlyHistory('EUR').subscribe((data) => {
      const timeSeries = data['Time Series FX (Monthly)'];
      const labels = [];
      const prices = [];

      for (const date in timeSeries) {
        labels.push(date);
        prices.push(parseFloat(timeSeries[date]['4. close']));
      }

      // Invertir para tener las fechas en orden cronológico
      this.lineChartData.labels = labels.reverse();
      this.lineChartData.datasets[0].data = prices.reverse();
    });
  }
}
