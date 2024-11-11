import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../../services/auth.service';
import {Iuser} from '../../../Interfaces/iuser';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-sing-in',
  templateUrl: './sing-in.component.html',
  styleUrl: './sing-in.component.css'
})
export class SingInComponent{
  loginForm: FormGroup = new FormGroup({});
  user:Iuser={
    name:'',
    lastName:'',
    email:'',
    password:'',
    fechaNacimiento:new Date(),
    img:''
  }


  constructor(private authService: AuthService,private fb: FormBuilder) {

    this.loginForm = this.fb.group({
      name: [''],
      lastName: [''],
      email: ['', [Validators.required, Validators.email]],
      password: [''],
      fechaNacimiento: [''],
      img: ['']
    });

  }



  onSubmit(): void {
    this.authService.signIn(
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
        //Si el registro es correcto, redirige al login

      });



  }

}
