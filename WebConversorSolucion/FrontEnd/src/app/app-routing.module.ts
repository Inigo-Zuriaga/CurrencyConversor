import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MainPageComponent} from './pages/main-page/main-page.component';
import {LoginComponent} from './pages/auth/login/login.component';
import {ConversorComponent} from './pages/conversor/conversor.component';
<<<<<<< HEAD
=======
import { AboutComponent } from './pages/about/about.component';
>>>>>>> parent of 7141ebd (merge. Paginas y enrutamiento. (conversor esta comentado para que no funcione))

//Aqui añadimos las rutas de la app. Asociamos cada ruta con un componente.
//Si la ruta necesita un parámetro, se pone nombreAccion/:nombreDelParametro
const routes: Routes = [
  {path:'',component:MainPageComponent},
  {path:'login',component:LoginComponent},
  {path:'conversor',component:ConversorComponent},
<<<<<<< HEAD
=======
  { path: 'about', component: AboutComponent },
  { path: 'login', component: LoginComponent },
>>>>>>> parent of 7141ebd (merge. Paginas y enrutamiento. (conversor esta comentado para que no funcione))
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
