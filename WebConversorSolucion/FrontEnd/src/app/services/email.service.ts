import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient, HttpParams} from '@angular/common/http';
import {environment} from '../environments/environment';

@Injectable({
  providedIn: 'root',
})

export class EmailService {

  private apiEmail=environment.apiUrl5; // API/Email

  constructor(private http: HttpClient) {

  }
  subject:string="";
  body:string="";
  sendRegisterEmail(email:string):Observable<any>{

    this.subject="Registro en Conversor de Divisas";

    // this.body=`Gracias por registrarte en Conversor de Divisas.`;
    this.body = `
¡Gracias por registrarte en Currency Converter! 🌟 Estamos emocionados de tenerte a bordo.

Con Currency Converter, puedes:
- Convertir monedas en tiempo real con las tasas de cambio más actualizadas.
- Guardar tus conversiones y acceder a tu historial de transacciones.

### Tus credenciales:
Correo electrónico: ${email}

Si tienes alguna pregunta o necesitas ayuda, no dudes en contactarnos a través de nuestro equipo de soporte en **currencyconversor@gmail.com**.

Haz clic en el enlace de abajo para comenzar a explorar nuestras funcionalidades:
👉 [Inicia Sesión en Currency Conversor](https://currency-conversor.vercel.app/login)

Gracias por elegirnos para tus necesidades de conversión de divisas. ¡Esperamos ayudarte a convertir tus sueños en realidad, moneda por moneda!

Saludos cordiales,
El equipo de Currency Conversor
`;

    const params = new HttpParams()
      .set('email', email)
      .set('theme', this.subject)
      .set('body', this.body);
    return this.http.post(`${this.apiEmail}/SendEmail`,null,{params});
    // return this.http.post(`http://localhost:25850/api/Email/SendEmail`,null,{params});
  }

  contactUs(email:string,subject:string,body:string):Observable<any>{

    body=`Cliente: ${email}\n\n${body}`;

    console.log("Cuerpo del mensaje modificado:", body);
    console.log("email:", email);
    const params = new HttpParams()
      .set('theme', subject)
      .set('body', body);

    return this.http.post(`${this.apiEmail}/Contact`,null,{params});
    // return this.http.post(`http://localhost:25850/api/Email/Contact`,null,{params});
  }

}
