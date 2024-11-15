import {Component, OnInit} from '@angular/core';
import { AuthService } from '../../services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrl: './history.component.css'
})
export class HistoryComponent implements OnInit {


  constructor(private authService: AuthService,private route:Router) { }

  historyData: any[] = [];
  email:string ='';

  ngOnInit():void {
    if (!this.authService.UserIsLogged()){
      this.route.navigate(['/']);
    }
    console.log("No puedes ver el historial si no estás logueado");
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

}
