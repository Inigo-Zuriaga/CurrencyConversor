import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MainPageComponent} from './pages/main-page/main-page.component';
import {LoginComponent} from './pages/auth/login/login.component';
import {ConversorComponent} from './pages/conversor/conversor.component';
import {RegisterComponent} from './pages/auth/register/register.component';
import {HistoryComponent} from './pages/history/history.component';
import {NotFoundComponent} from './pages/not-found/not-found.component';
import {AboutComponent} from './pages/about/about.component';
import {ProfileComponent} from './pages/auth/profile/profile.component';
import {ContactFormComponent} from './pages/contact-form/contact-form.component';
//Aqui añadimos las rutas de la app. Asociamos cada ruta con un componente.
//Si la ruta necesita un parámetro, se pone nombreAccion/:nombreDelParametro
const routes: Routes = [
  {path:'',component:MainPageComponent},
  {path:'login',component:LoginComponent},
  {path:'conversor',component:ConversorComponent},
  {path: 'login', component: LoginComponent },
  {path: 'register', component: RegisterComponent },
  {path: 'history', component: HistoryComponent },
  {path: 'about', component: AboutComponent },
  {path: 'profile', component: ProfileComponent },
  {path: 'contact-us', component: ContactFormComponent },
  {path: 'not-found', component:NotFoundComponent },
  {path: '**', component:NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
