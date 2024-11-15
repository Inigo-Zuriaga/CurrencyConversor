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
  constructor(private authService: AuthService) {}
  // this.authService.getAccessToken();

  ngOnInit():void {

    this.userSub = this.authService.isLogged.subscribe((value) => {
      this.isLoged = value; // Actualiza automáticamente
    });
    this.isLoged = this.authService.UserIsLogged();
    this.userSub=this.authService.isLogged.subscribe({
      next: (value):any => {
        this.isLoged=value;
      }

    })
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
