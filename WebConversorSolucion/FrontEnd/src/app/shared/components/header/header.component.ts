import { Component,OnInit,OnDestroy} from '@angular/core';
import { AuthService} from '../../../services/auth.service';
import {debounceTime, Observable, Subscription} from 'rxjs';
import {Idropdownoption} from '../../../Interfaces/idropdownoption';
import {Router} from '@angular/router';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})

export class HeaderComponent implements OnInit,OnDestroy{
  isMenuOpen = false;
  userSub!: Subscription;
  isLoged:boolean= false;
  imageSrc :string='' ;


  // Opciones del dropdown
  options: Idropdownoption[] = [
    { label: 'Perfil', route: '/profile' },
    { label: 'Cerrar Sesión', callback: () => this.disconnect() }
  ];
  constructor(private authService: AuthService,private router:Router) {

    if (this.authService.UserIsLogged()) {
      this.authService.getUserData().subscribe((data:any) => {
        this.authService.photoSubject.next(data.img);
      });
      }
  }
  ngOnInit():void {
    //
    this.userSub = this.authService.isLogged.subscribe((value) => {
      this.isLoged = value; // Actualiza automáticamente
    });

    this.userSub=this.authService.isLogged.subscribe({
      next: (value):any => {
        this.isLoged=value;

        this.authService.getUserData().subscribe({
          next: (data):any => {

          console.log("Los Datossss:", data);

          this.imageSrc = data.img; // Carga la foto del usuario
        }});

      }

    })

    this.authService.photoData$.subscribe({
      next: (value):any => {

      this.imageSrc = value;
    }});

    //Nos suscibimos al BehaviorSubject para obtener la foto de perfil


    this.isLoged = this.authService.UserIsLogged();
  }

  disconnect(){
    console.log("Cerrando sesión, eliminando token...");
    this.authService.deleteToken();
    localStorage.clear(); // Limpia el localStorage completamente
    // this.authService.photoSubject.next('');
    this.userSub.unsubscribe();
    this.authService.logged.next(false);
    this.imageSrc ='' ;
    this.router.navigate(['/']);
  }
  ngOnDestroy(): void {
      this.userSub.unsubscribe();
  }
  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
}
