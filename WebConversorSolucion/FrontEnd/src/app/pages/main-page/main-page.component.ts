import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import {Subscription} from 'rxjs';
import {ChartComponent} from '../../shared/chart/chart.component';
//Como indique antes, el nombre del componente lo encontramos en el selector
@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.css'
})
export class MainPageComponent implements OnInit, OnDestroy {
  userSub!: Subscription;
  isLoged: boolean = false;
  conversions: any[] = [];
  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    // Suscribirse a los cambios de autenticación
    this.userSub = this.authService.isLogged.subscribe((value) => {
      this.isLoged = value;
    });

    // Verificar el estado actual de autenticación
    this.isLoged = this.authService.UserIsLogged();
  }

  ngOnDestroy(): void {
    // Cancelar la suscripción al salir del componente
    this.userSub.unsubscribe();
  }
}
