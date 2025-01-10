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
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {loginInterceptor} from './Interceptors/login.interceptor';
import {AuthInterceptor} from './Interceptors/auth.interceptor';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import { CurrencyChartComponent } from './currency-chart/currency-chart.component';
import { NgChartsModule } from 'ng2-charts';
import { RegisterComponent } from './pages/auth/register/register.component';
import { HistoryComponent } from './pages/history/history.component';
import {provideAnimationsAsync} from '@angular/platform-browser/animations/async';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { ProfileComponent } from './pages/auth/profile/profile.component';
import { AboutComponent } from './pages/about/about.component';
import { HistorytableComponent } from './shared/components/historytable/historytable.component';
import { CarouselModule } from 'primeng/carousel';
import { ChartModule } from 'primeng/chart';
import { ButtonModule } from 'primeng/button';
import {TagModule} from 'primeng/tag';
import { DropdownComponent } from './shared/components/dropdown/dropdown.component';
import { ChartComponent } from './shared/chart/chart.component';
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
    CurrencyChartComponent,
    RegisterComponent,
    HistoryComponent,
    NotFoundComponent,
    AboutComponent,
    ProfileComponent,
    HistorytableComponent,
    ProfileComponent,
    DropdownComponent,
    ChartComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    NgChartsModule,
    NgOptimizedImage,
    CarouselModule,
    ButtonModule,
    TagModule,
    ChartModule
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
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
