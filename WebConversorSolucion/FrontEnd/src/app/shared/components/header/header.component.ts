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

  constructor(private authService: AuthService) {}
  // this.authService.getAccessToken();
  userSub!: Subscription;
  isLoged:boolean= false;
  ngOnInit():void {

    this.userSub=this.authService.logged.subscribe({

      next: (value) => {
        this.isLoged=value;
      }

    })

    // this.isLoged= this.authService.UserIsLogged();
  }
  ngOnDestroy(): void {
      this.userSub.unsubscribe();
  }
  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
}
