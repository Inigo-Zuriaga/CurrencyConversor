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


  // this.authService.getAccessToken();
  ngOnInit():void {
    //
    this.userSub = this.authService.isLogged.subscribe((value) => {
      this.isLoged = value; // Actualiza automáticamente
    });

    this.userSub=this.authService.isLogged.subscribe({
      next: (value):any => {
        this.isLoged=value;

        // this.authService.getUserData().pipe(debounceTime(2000)).subscribe((data) => {
        //   console.log("Los Datossss:", data);
        //   this.imageSrc = data.img; // Carga la foto del usuario
        // });
        this.authService.getUserData().pipe(debounceTime(2000)).subscribe({
          next: (data):any => {

          console.log("Los Datossss:", data);
          this.imageSrc = data.img; // Carga la foto del usuario
        }});

      }

    })

    // this.isLoged = this.authService.UserIsLogged();
    // this.userSub=this.authService.isLogged.pipe(debounceTime(2000)).subscribe({
    //   next: (value):any => {
    //     this.isLoged=value;
    //
    //     this.authService.getUserData().pipe(debounceTime(2000)).subscribe((data) => {
    //       console.log("Los Datossss:", data);
    //       this.imageSrc = data.img; // Carga la foto del usuario
    //     });
    //
    //   }
    //
    // })

    //ESTO ESTABA ANTES
    // this.authService.photoData$.pipe(debounceTime(2000)).subscribe((photo) => {
    //   this.imageSrc = photo;
    // });

    this.authService.photoData$.pipe(debounceTime(2000)).subscribe({
      next: (value):any => {

      this.imageSrc = value;
    }});
    //Obtenemos la foto de perfil del usuario en tiempo real
    // this.authService.getUserData().subscribe((data) => {
    //   console.log("Los Datossss:", data);
    //   this.imageSrc = data.img; // Carga la foto inicial
    // });
    //Nos suscibimos al BehaviorSubject para obtener la foto de perfil


    this.isLoged = this.authService.UserIsLogged();
  }
  // getUserData():Observable<any>{
  //
  //   // return this.http.post(`${this.apiUrl}/GetUser`, JSON.stringify(email));
  //   return this.http.get(`${this.apiUrl}/GetUserData`);
  //
  //
  // }

  disconnect(){
    this.authService.deleteToken();
    this.userSub.unsubscribe();
    this.authService.logged.next(false);
    // this.imageSrc ='' ;
    this.router.navigate(['/']);
  }
  ngOnDestroy(): void {
      this.userSub.unsubscribe();
  }
  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
}
