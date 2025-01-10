import { Component,OnInit,ChangeDetectorRef } from '@angular/core';
import {AuthService} from '../../services/auth.service';

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

  constructor( private authService: AuthService, private cd: ChangeDetectorRef) {
  }

  ngOnInit(): void {

    this.extractAndCountCoins();
    this.initChart();
  }

  initChart() {
    this.data = {
      labels: ['A', 'B', 'C'],
      datasets: [
        {
          data: [540, 325, 702],
          backgroundColor: ['blue', '#FF3860', 'yellow'],
          // hoverBackgroundColor:['#00D1B2','#FF3860','#00D1B2']
        }
      ]
    }
  }




  prueba: any[] = []
  extractAndCountCoins(){

    this.authService.viewHistory(this.authService.getUserEmail()).subscribe(
      (data) => {

        this.prueba = data;

        console.log("Datos recibidos2222:", this.prueba);
        // console.log("Primera conversión:", this.prueba[0]);

        console.log("CONVERSIONS ARRAY: ", this.prueba);

        this.prueba.forEach(prueba => {

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

        console.log("LAS MONEDAS :",this.coinCount);
      },
      (error) => {
        console.error("Error mandar las monedas:", error);
      }
    )


  }

}
