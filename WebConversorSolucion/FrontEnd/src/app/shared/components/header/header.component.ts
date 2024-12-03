import { Component,OnInit,OnDestroy} from '@angular/core';
import { AuthService} from '../../../services/auth.service';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})

export class HeaderComponent implements OnInit,OnDestroy{
  isMenuOpen = false;
  userSub!: Subscription;
  isLoged:boolean= false;
  profilePhoto: string = '';
  constructor(private authService: AuthService) {

    if (this.authService.UserIsLogged()) {
      this.authService.getUserData().subscribe((data:any) => {
        this.authService.photoSubject.next(data.img); // Asumiendo que `img` es la propiedad de la foto
      });
      }
  }
  // this.authService.getAccessToken();

  imageSrc :string='' ;
  ngOnInit():void {

    this.userSub = this.authService.isLogged.subscribe((value) => {
      this.isLoged = value; // Actualiza automáticamente
    });
    this.isLoged = this.authService.UserIsLogged();
    this.userSub=this.authService.isLogged.subscribe({
      next: (value):any => {
        this.isLoged=value;

        if(this.isLoged){
          this.authService.getUserData().subscribe({
            next: (data) => {
              console.log("Datos recibidos 222:", data);
              this.imageSrc = data.img;
              console.log("Imagen",this.imageSrc);
            },
            error: (error) => {
              console.error("Error al obtener el historial:", error);
            }
          });
        }
      }

    })
    this.authService.photoData$.subscribe((photo) => {
      console.log("Foto de perfil actualizada en el header:", photo);
      this.profilePhoto = photo;
    });
    this.authService.getUserData().subscribe((data) => {
      this.profilePhoto = data.img; // Asumiendo que `img` es la propiedad de la foto
    });
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
