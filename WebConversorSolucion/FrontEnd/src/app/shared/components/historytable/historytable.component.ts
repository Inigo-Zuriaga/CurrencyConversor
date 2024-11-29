import { Component,Input  } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
@Component({
  selector: 'app-historytable',
  templateUrl: './historytable.component.html',
  styleUrl: './historytable.component.css'
})
export class HistorytableComponent {
@Input() historyData: any[] = [];
@Input() limit: number | null = null;

constructor(private authService: AuthService) {
}
  get displayedData() {
    // Retorna todos los datos o un subconjunto limitado
    return this.limit ? this.historyData.slice(0, this.limit) : this.historyData;
  }
  // email='';
  // onSubmit(){
  //   //Obtengo el email del usuario desde el token
  //   this.email= this.authService.getUserEmail();
  //
  //   console.log("ESTE ES EL EMAIL",this.email);
  //   this.authService.viewHistory(this.email).subscribe(
  //     (data) => {
  //       console.log("Datos recibidos:", data);
  //       this.historyData = data;
  //
  //     },
  //     (error) => {
  //       console.error("Error al obtener el historial:", error);
  //     });
  // }
}
