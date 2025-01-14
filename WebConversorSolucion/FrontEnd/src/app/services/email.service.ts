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
  sendEmail(email:string,subject:string,body:string):Observable<any>{

    const params = new HttpParams()
      .set('email', email)
      .set('theme', subject)
      .set('body', body);

    // console.log("El data");
    // console.log(data);
    // return this.http.post(`${this.apiEmail}/SendEmail`,data);
    // return this.http.post(`${this.apiEmail}/SendEmail`,null,{params});
    return this.http.post(`http://localhost:25850/api/Email/SendEmail`,null,{params});
  }

  contactUs(email:string,subject:string,body:string):Observable<any>{

    const params = new HttpParams()
      .set('email', email)
      .set('theme', subject)
      .set('body', body);

    // console.log("El data");
    // console.log(data);
    // return this.http.post(`${this.apiEmail}/SendEmail`,data);
    return this.http.post(`${this.apiEmail}/Contact`,null,{params});
    // return this.http.post(`http://localhost:25850/api/Email`,null,{params});
  }

}
