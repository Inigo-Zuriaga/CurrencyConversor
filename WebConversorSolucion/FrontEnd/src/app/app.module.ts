import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { HeaderComponent } from './shared/components/header/header.component';
import { FooterComponent } from './shared/components/footer/footer.component';
import { ConversorComponent } from './pages/conversor/conversor.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import { PruebaComponent } from './prueba/prueba.component';
import { CurrencySelectorComponent } from './shared/components/currency-selector/currency-selector.component';
import {loginInterceptor} from './Interceptors/login.interceptor';
import {AuthInterceptor} from './Interceptors/auth.interceptor';

//En este Archivo importaremos todos los componentes que creemos y
//los añadiremos a la lista de declarations.

//En el apartado de imports añdadiremos Modelos (No vais a tener que meter muchos)
@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    LoginComponent,
    HeaderComponent,
    FooterComponent,
    ConversorComponent,
    PruebaComponent,
    CurrencySelectorComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: loginInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
