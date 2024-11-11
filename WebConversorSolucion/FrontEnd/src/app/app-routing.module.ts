import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MainPageComponent} from './pages/main-page/main-page.component';
import {LoginComponent} from './pages/auth/login/login.component';
import {ConversorComponent} from './pages/conversor/conversor.component';
import {RegisterComponent} from './pages/auth/register/register.component';
//Aqui añadimos las rutas de la app. Asociamos cada ruta con un componente.
//Si la ruta necesita un parámetro, se pone nombreAccion/:nombreDelParametro
const routes: Routes = [
  {path:'',component:MainPageComponent},
  {path:'login',component:LoginComponent},
  {path:'conversor',component:ConversorComponent},
  {path: 'login', component: LoginComponent },
  {path: 'register', component: RegisterComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
