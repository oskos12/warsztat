import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddComponent } from './components/cars/add-car/add.component';
import { CarsListComponent } from './components/cars/cars-list/cars-list.component';
import { AddClientComponent } from './components/clients/add-client/add-client.component';
import { ClientsListComponent } from './components/clients/clients-list/clients-list.component';
import { ContactComponent } from './components/contact/contact.component';
import { HomeComponent } from './components/home/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { OfferComponent } from './components/offer/offer.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ServiceComponent } from './components/service-query/service.component';
import { ServiceViewComponent } from './components/service-view/service-view.component';
import { AddServiceComponent } from './components/services/add-service/add-service.component';
import { ServicesListComponent } from './components/services/services-list/services-list.component';
import { SignupComponent } from './components/signup/signup.component';
import { WorkersComponent } from './components/workers/workers.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'cars', component: CarsListComponent, canActivate:[AuthGuard]},
  {path: 'login', component: LoginComponent},
  {path: 'signup', component: SignupComponent},
  {path: 'addCar', component: AddComponent},
  {path: 'zapytanie', component: ServiceComponent},
  {path: 'profil', component: ProfileComponent},
  {path: 'kontakt', component: ContactComponent},
  {path: 'oferta', component: OfferComponent},
  {path: 'services', component: ServicesListComponent},
  {path: 'addService', component: AddServiceComponent},
  {path: 'clients', component: ClientsListComponent},
  {path: 'addClient', component: AddClientComponent},
  {path: 'zapytania', component: ServiceViewComponent},
  {path: 'workers', component: WorkersComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
