import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Car } from 'src/app/models/cars.model';
import { Client } from 'src/app/models/clients.model';
import { Dictionary } from 'src/app/models/dictionary.model';
import { ServicesWithWorkers } from 'src/app/models/service.model';
import { AuthService } from 'src/app/services/auth.service';
import { CarsService } from 'src/app/services/cars.service';
import { DictionaryService } from 'src/app/services/dictionary.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-cars-list',
  templateUrl: './cars-list.component.html',
  styleUrls: ['./cars-list.component.css']
})
export class CarsListComponent implements OnInit {
  cars: Car[] = [];
  public fullName : string = "";
  public role : string = "";
  public sid : string = ""; 
  vehicleStatuses!: Dictionary[];
  public history: boolean = false;
  services: ServicesWithWorkers[] = [];
  user: boolean = false;
  client: string[] = [];
  cname: string = "";
  constructor(private carsService: CarsService, private router: Router, private auth: AuthService, private userStore: UserStoreService, private toast: NgToastService, private dict: DictionaryService) {}

  ngOnInit(): void {
    this.dict.getDictionary('Status pojazdu')
    .subscribe({
      next: (res=>{
        this.vehicleStatuses = res.result.data;
      })
    })

    this.userStore.getFullNameFromStorage()
    .subscribe(val=>{
      const fullNameFromToken = this.auth.getFullNameFromToken();
      this.fullName = val || fullNameFromToken;
    })

    this.userStore.getRoleFromStorage()
    .subscribe(val=>{
      const roleFromToken = this.auth.getRoleFromToken();
      this.role = val || roleFromToken;
    })

    this.userStore.getSidFromStorage()
    .subscribe(val=>{
      const sidFromToken = this.auth.getSidFromToken();
      this.sid = val || sidFromToken;
    })

    if(this.role === "Admin" || this.role === "Personel"){
      this.carsService.getAllCars().subscribe({
        next: (res=>{
          this.cars = res.result.data;
          console.log(this.cars);
        }),
        error: (response) => {
          console.log(response);
        }
      })
    }
    else{
      this.carsService.getCars(this.sid).subscribe({
        next: (res=>{
          if(res.statusCode != 404)
            this.cars = res.result.data;
        }),
        error: (response) => {
          console.log(response);
        }
      })
    }
    
  }

  powrot(){
    this.history = false;
    this.services = [];
  }
  addNew(){
    this.router.navigate(['addCar'])
  }
  viewHistory(car: Car){
    this.history = true;
    this.carsService.getServices(car.id).subscribe({
      next: (res=>{
        if(res.statusCode != 404)
          this.services = res.result.data;
      }),
      error: (response) => {
        console.log(response);
      }
    })
  }
  edit(car: Car){
    
  }
  active(car: Car){
    this.carsService.active(car.id)
    .subscribe({
      next:(res)=>{
        this.toast.success({detail:"Powodzenie", summary:"Zaktualizowano aktywność pojazdu", duration:2000})
      },
      error:(err)=>{
        this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
      }
    })
  }
  type(car: Car, type: Dictionary){
    this.carsService.type(car.id, type.id)
    .subscribe({
      next:(res)=>{
        if(type.value === "User")
          this.router.navigate(['clients']);
        this.toast.success({detail:"Powodzenie", summary:"Zaktualizowano status pojazdu", duration:2000})
      },
      error:(err)=>{
        this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
      }
    })
  }

  getClient(client: Client, fullName: string){
    this.auth.getClientByIdDetails(client.id)
    .subscribe({
      next:(res)=>{
        this.client = res.result.data;
        console.log(Object.values(res.result.data))
        this.client = Object.values(res.result.data);
      }
    })
    this.cname = fullName;
    this.user = true;

  }

  back(){
    this.user = false;
  }
}
