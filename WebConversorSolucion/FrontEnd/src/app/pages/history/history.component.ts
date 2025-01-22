import {Component, OnInit} from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { ExchangeService } from '../../services/exchange.service';
import {Router} from '@angular/router';
import Swal from 'sweetalert2';
import { History } from '../../Interfaces/ihistory';
@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrl: './history.component.css'
})
export class HistoryComponent implements OnInit    {


  dataPdf: History[] = [];

  constructor(private authService: AuthService,private exchangeService:ExchangeService,private route:Router) { }

  historyData: any[] = [];
  email:string ='';
  htmlContent:string='';



  ngOnInit():void {

    if (!this.authService.UserIsLogged()){
      this.route.navigate(['/']);
    }
    this.onSubmit();
  }

  onSubmit(){
    //Obtengo el email del usuario desde el token
   this.email= this.authService.getUserEmail();

    this.authService.getUserData().subscribe((userData) => {

      console.log("ESTE ES EL EMAIL", this.email);
      this.authService.viewHistory(this.email).subscribe(
        (data) => {
          console.log("Datos recibidos:", data);
          this.historyData = data;

          this.dataPdf = data.map((item: any) => ({
            fromCoin: item.fromCoin,          // El tipo de moneda de la transacción
            fromAmount: item.fromAmount,      // La cantidad de la moneda de origen
            toCoin: item.toCoin,              // El tipo de moneda de destino
            toAmount: item.toAmount,          // La cantidad de la moneda de destino
            date: item.date,                  // La fecha de la transacción
            email: item.user.email,           // El email del usuario relacionado con la transacción
            name: userData.name,
            lastName: userData.lastName
          }));

        },
        (error) => {
          console.error("Error al obtener el historial:", error);
        });

    }, (error) => {
      console.error("Error al obtener el historial:", error);
    });
  }


crearPdf(){

  console.log("El history"+this.dataPdf);
    this.exchangeService.createPdf(this.dataPdf).subscribe(
      (data:Blob) => {
        const blob = new Blob([data], { type: 'application/pdf' });
        const url = window.URL.createObjectURL(blob);

        const a = document.createElement('a');
        a.href = url;
        this.email= this.authService.getUserEmail();
        a.download = 'conversionHistory.pdf'; // Nombre del archivo
        a.click();
        window.URL.revokeObjectURL(url);
      }
    );
}

  deleteHistory(id:number){


    Swal.fire({
      title: "Are you sure you what to delete it?",
      // text: "Se borrara el registro!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      cancelButtonText: "Delete",
      confirmButtonText: "Cancel"
    }).then((result:any) => {

      if (result.isConfirmed) {

        this.exchangeService.deleteHistory(id).subscribe(
          (data) => {
            console.log("Historial eliminado:", data);
            this.onSubmit();
          },
          (error) => {
            console.error("Error al borrar el historial:", error);
            Swal.fire({
              title: "Error!",
              text: "Error al borrar el registro.",
              icon: "error"
            });
          });

      }});
  }

}
