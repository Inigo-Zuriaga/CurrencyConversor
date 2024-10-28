import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {LoginComponent} from "./pages/auth/login/login.component";
import {AppComponent } from './app.component';
import {MainPageComponent} from './pages/main-page/main-page.component';
import {PageNotFoundComponent} from './pages/page-not-found/page-not-found.component';
import {ConversorComponent} from './pages/conversor/conversor.component';

//Indicamos todas las rutas que cargaran el componente que indiquemos
export const routes: Routes = [
  {
    path: '',
    component: MainPageComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  { path: 'conversor',
    component: ConversorComponent
  },
  {
    path: '404',
    component: PageNotFoundComponent,
  },
  {
    path: '**',
    redirectTo: '/404',
  }

];
