import { Component,OnInit,OnDestroy} from '@angular/core';
import { AuthService} from '../../../services/auth.service';
import {Subscription} from 'rxjs';
import {Idropdownoption} from '../../../Interfaces/idropdownoption';

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
  constructor(private authService: AuthService) {

    if (this.authService.UserIsLogged()) {
      this.authService.getUserData().subscribe((data:any) => {
        this.authService.photoSubject.next(data.img);
      });
      }
  }
  // this.authService.getAccessToken();
  ngOnInit():void {

    this.userSub = this.authService.isLogged.subscribe((value) => {
      this.isLoged = value; // Actualiza automáticamente
    });

    this.isLoged = this.authService.UserIsLogged();
    this.userSub=this.authService.isLogged.subscribe({
      next: (value):any => {
        this.isLoged=value;

        this.authService.getUserData().subscribe((data) => {
          console.log("Los Datossss:", data);
          this.imageSrc = data.img; // Carga la foto del usuario
        });
      }
    })

    this.authService.photoData$.subscribe((photo) => {
      this.imageSrc = photo;
    });

    //Obtenemos la foto de perfil del usuario en tiempo real
    // this.authService.getUserData().subscribe((data) => {
    //   console.log("Los Datossss:", data);
    //   this.imageSrc = data.img; // Carga la foto inicial
    // });
    //Nos suscibimos al BehaviorSubject para obtener la foto de perfil


    this.isLoged = this.authService.UserIsLogged();
  }

  disconnect(){
    this.authService.deleteToken();
    this.userSub.unsubscribe();
  }
  ngOnDestroy(): void {
      this.userSub.unsubscribe();
  }
  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
}
