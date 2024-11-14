import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrl: './history.component.css'
})
export class HistoryComponent {


  constructor(private authService: AuthService) { }
  historyData: any[] = [];
  email:string ="ggrg2@gmail.com";
  onSubmit(){
    this.authService.viewHistory(this.email).subscribe(
      (data) => {
        console.log("Datos recibidos:", data);
        this.historyData = data;
      },
      (error) => {
        console.error("Error al obtener el historial:", error);
    });

  }

}
