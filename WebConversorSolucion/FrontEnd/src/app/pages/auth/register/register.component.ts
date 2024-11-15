import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../../services/auth.service';
import {Iuser} from '../../../Interfaces/iuser';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})

export class RegisterComponent{
  loginForm: FormGroup = new FormGroup({}); // declara el formulario de registro
  user:Iuser={
    name:'',
    lastName:'',
    email:'',
    password:'',
    fechaNacimiento:new Date(),
    img:''
  }

  // Constructor que inyecta el AuthService y FormBuilder para construir el formulario
  constructor(private authService: AuthService,private fb: FormBuilder,private route:Router) {
    // definir la estructura del form con sus campos y validaciones
    this.loginForm = this.fb.group({
      name: [''],
      lastName: [''],
      email: ['', [Validators.required, Validators.email]],
      password: [''],
      fechaNacimiento: [''],
      img: ['']
    });

  }

  ngOnInit(): void {
    if (this.authService.UserIsLogged()){

      this.route.navigate(['/']).then(r => { })
    }
  }

  // función que se llama al enviar el form
  onSubmit(): void {
    this.authService.signIn( //se llama ha este método y se le pasan los valores del form
      this.loginForm.value.name,
      this.loginForm.value.lastName,
      this.loginForm.value.email,
      this.loginForm.value.password,
      this.loginForm.value.fechaNacimiento,
      this.loginForm.value.img
    )
      .subscribe(
        (data) => {
          //hola
        console.log(data);

          //Comprobar si funciona dando errores
          // this.route.navigate(['/'])
        //Si el registro es correcto, redirige al login

      });

  }

}
