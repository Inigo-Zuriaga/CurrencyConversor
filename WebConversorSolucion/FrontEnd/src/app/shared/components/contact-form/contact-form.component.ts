import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../../../services/auth.service';
import {Router} from '@angular/router';
import {EmailService} from '../../../services/email.service';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrl: './contact-form.component.css'
})
export class ContactFormComponent {
  showWindow = false;
  contactForm: FormGroup;

  // constructor que recive FormBuilder y AuthService
  constructor(private fb: FormBuilder, private authService: AuthService,
              private route: Router,private emailService:EmailService) {
    // inicializa el formulario de login usando FormBuilder
    this.contactForm = this.fb.group({
      Subject: [''],
      Body: [''],
    });
  }
  email: string = '';
  onSubmit() {
    const EmailData = {
      Subject: this.contactForm.value.Subject,
      Body: this.contactForm.value.Body,
    };
    console.log("Llega aqui")

    this.email = this.authService.getUserEmail();

    this.emailService.sendEmail(this.email,EmailData.Subject, EmailData.Body).subscribe(

      (data) => {
        console.log("Respuesta del backend:", data);

      },
      (error) => {
        console.error("Error en el envio del email:", error);
      }

    );
  }


  toggleWindow(): void {
    this.showWindow = !this.showWindow;
  }
}
