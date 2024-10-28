import { Component ,OnInit} from '@angular/core';
// import {AppComponent } from '../app.component';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent implements OnInit{


  ngOnInit() {

    console.log("HOLA")
  }


}
