import {Component, OnInit} from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { ExchangeService } from '../../services/exchange.service';
import {Router} from '@angular/router';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrl: './history.component.css'
})
export class HistoryComponent implements OnInit {


  constructor(private authService: AuthService,private exchangeService:ExchangeService,private route:Router) { }

  historyData: any[] = [];
  email:string ='';

  ngOnInit():void {

    if (!this.authService.UserIsLogged()){
      this.route.navigate(['/']);
    }
    this.onSubmit();
  }


  onSubmit(){
    //Obtengo el email del usuario desde el token
   this.email= this.authService.getUserEmail();

   console.log("ESTE ES EL EMAIL",this.email);
    this.authService.viewHistory(this.email).subscribe(
      (data) => {
        console.log("Datos recibidos:", data);
        this.historyData = data;
      },
      (error) => {
        console.error("Error al obtener el historial:", error);
    });
  }


  deleteHistory(id:number){


    Swal.fire({
      title: "Estas seguro?",
      text: "Se borrara el registro!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Confirmar"
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
