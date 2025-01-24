import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrl: './chart.component.css'
})
export class ChartComponent implements OnInit {
  data: any;
  options: any;
  toCoins: string[] = [];
  coinCount: { [key: string]: number } = {};
  accessToken: string | null = null;

  constructor(private authService: AuthService, private cd: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.accessToken = this.authService.getAccessToken(); // Obtener el accessToken desde el AuthService

    if (!this.accessToken) {
      console.warn("Access token not available. Chart will not be initialized.");
      return; // Detener la ejecución si no hay token
    }

    this.extractAndCountCoins();
  }

  initChart() {
    const labels = Object.keys(this.coinCount);
    const data = Object.values(this.coinCount);
    console.log("Labels:", labels);
    const backgroundColor = ['blue', '#FF3860', 'orange', 'green', 'purple', 'orange']; // Add more colors as needed

    this.data = {
      labels: labels,
      datasets: [
        {
          data: data,
          backgroundColor: backgroundColor.slice(0, labels.length), // Ensure the number of colors matches the number of labels
        }
      ]
    };
  }

  convertions: any[] = [];
  extractAndCountCoins() {
    this.authService.viewHistory(this.authService.getUserEmail()).subscribe(
      (data) => {
        this.convertions = data;

        this.convertions.forEach((prueba) => {
          const coin = prueba.toCoin;
          // Almacenar el coin en el array
          this.toCoins.push(coin);

          // Contar las repeticiones
          if (this.coinCount[coin]) {
            this.coinCount[coin]++;
          } else {
            this.coinCount[coin] = 1;
          }
        });

        console.log("LAS MONEDAS :", this.coinCount);
        this.initChart();
      },
      (error) => {
        console.error("Error al mandar las monedas:", error);
      }
    );
  }
};
