import {Component} from '@angular/core';
import {AuthService} from '../../../services/auth.service';
import {Iuser} from '../../../Interfaces/iuser';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})

export class RegisterComponent/* implements OnInit*/{
  loginForm: FormGroup = new FormGroup({}); // declara el formulario de registro
  user:Iuser={
    name:'',
    lastName:'',
    email:'',
    password:'',
    fechaNacimiento:new Date(),
    img:''
  }
  selectedPicture: string = '';
  // Constructor que inyecta el AuthService y FormBuilder para construir el formulario
  constructor(private authService: AuthService,private fb: FormBuilder,private route:Router) {
    // definir la estructura del form con sus campos y validaciones
    this.loginForm = this.fb.group({
      name: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],  // Email es obligatorio y debe tener un formato válido
      password: ['', [Validators.required, Validators.minLength(6)]],  // Contraseña obligatoria, mínimo 6 caracteres
      fechaNacimiento: [''],
      img: ['']  // La imagen no es obligatoria
    });
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
          this.route.navigate(['/'])
      });
  }

  selectProfilePicture(picture: string) {
    this.selectedPicture = picture;
    this.loginForm.value.img= this.selectedPicture
    console.log(`Selected profile picture: ${picture}`);
  }

}
