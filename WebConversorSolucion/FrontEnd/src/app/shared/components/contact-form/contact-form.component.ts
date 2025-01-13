import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../../../services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrl: './contact-form.component.css'
})
export class ContactFormComponent {
  showWindow = false;
  contactForm: FormGroup;

  // constructor que recive FormBuilder y AuthService
  constructor(private fb: FormBuilder, private authService: AuthService, private route: Router) {
    // inicializa el formulario de login usando FormBuilder
    this.contactForm = this.fb.group({
      Subject: ['', [Validators.required,]],
      Body: ['', [Validators.required ]]
    });
  }

  onSubmit() {
    const EmailData = {
      Subject: this.contactForm.value.Subject,
      Body: this.contactForm.value.Body,
    };
  }


  toggleWindow(): void {
    this.showWindow = !this.showWindow;
  }
}
