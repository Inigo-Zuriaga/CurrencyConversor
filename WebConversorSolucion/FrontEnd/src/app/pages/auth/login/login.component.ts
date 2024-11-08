import { Component } from '@angular/core';
import {AuthService} from "../../../services/auth.service";
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

    email: string = '';
    password: string = '';
    errorMessage: string = '';
    constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService.login(this.email, this.password).subscribe(
      (response) => {
        // Si el login es exitoso, redirige a la página principal
        this.router.navigate(['/home']);
      },
      (error) => {
        // Maneja el error si las credenciales son incorrectas
        this.errorMessage = 'Correo o contraseña incorrectos.';
      }
    );
  }
}
