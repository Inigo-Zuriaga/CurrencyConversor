import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})

export class LoginComponent implements OnInit{
    loginForm: FormGroup;

  // constructor que recive FormBuilder y AuthService
  constructor(private fb: FormBuilder, private authService: AuthService, private route: Router) {
    // inicializa el formulario de login usando FormBuilder
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]], // Campo de email con validación de requerimiento y formato de email
      password: ['', [Validators.required, Validators.minLength(6)]], // Campo de contraseña con validación de requerimiento y longitud mínima de 6 caracteres
    });
  }

  ngOnInit(): void {
    if (this.authService.UserIsLogged()){

      this.route.navigate(['/']).then(r => { })
    }

    // this.onSubmit();
  }
  // Método para manejar el envío del formulario
  onSubmit() {
    const loginData = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password,
    };
  
    console.log("Datos enviados al backend:", loginData);
  
    this.authService.login(loginData.email, loginData.password).subscribe(
      (data) => {
        console.log("Respuesta del backend:", data);
        this.authService.storeToken(data.token);
        this.route.navigate(['/']);
      },
      (error) => {
        //console.error("Error en el login:", error);

        if (error.status === 401 || error.status === 400) {
          this.loginForm.setErrors({ serverError: error.error.error });
        } else {
          console.error("Error en el login:", error);
        }
      }

    );
  
    this.loginForm.reset();
  }
}