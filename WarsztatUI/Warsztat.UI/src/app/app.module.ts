import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CarsListComponent } from './components/cars/cars-list/cars-list.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { NgToastModule } from 'ng-angular-popup';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { HomeComponent } from './components/home/home/home.component';
import { AddComponent } from './components/cars/add-car/add.component';
import { ServiceComponent } from './components/service-query/service.component';
import { ProfileComponent } from './components/profile/profile.component';
import { OfferComponent } from './components/offer/offer.component';
import { ContactComponent } from './components/contact/contact.component';
import { ServicesListComponent } from './components/services/services-list/services-list.component';
import { AddServiceComponent } from './components/services/add-service/add-service.component';
import { ClientsListComponent } from './components/clients/clients-list/clients-list.component';
import { AddClientComponent } from './components/clients/add-client/add-client.component';
import { ServiceViewComponent } from './components/service-view/service-view.component';
import { WorkersComponent } from './components/workers/workers.component';

@NgModule({
  declarations: [
    AppComponent,
    CarsListComponent,
    LoginComponent,
    SignupComponent,
    HomeComponent,
    AddComponent,
    ServiceComponent,
    ProfileComponent,
    OfferComponent,
    ContactComponent,
    ServicesListComponent,
    AddServiceComponent,
    ClientsListComponent,
    AddClientComponent,
    ServiceViewComponent,
    WorkersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgToastModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi:true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
