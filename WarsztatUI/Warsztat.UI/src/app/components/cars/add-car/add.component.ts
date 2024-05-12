import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from 'src/app/helpers/validateForm';
import { Dictionary } from 'src/app/models/dictionary.model';
import { Client } from 'src/app/models/clients.model';
import { AuthService } from 'src/app/services/auth.service';
import { CarsService } from 'src/app/services/cars.service';
import { DictionaryService } from 'src/app/services/dictionary.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  addCarForm!: FormGroup;
  vehicleTypes!: Dictionary[];
  engineTypes!: Dictionary[];
  clients!: Client[];
  constructor(private fb: FormBuilder, private carService: CarsService, private router: Router, private toast: NgToastService, private dict: DictionaryService){}

  ngOnInit(): void {
    this.addCarForm = this.fb.group({
      brand: ['', Validators.required],
      model: ['', Validators.required],
      registration: ['', Validators.required],
      productionYear: ['', Validators.required],
      capacity: ['', Validators.required],
      owner: ['', Validators.required],
      vehicleType: ['', Validators.required],
      engineType: ['', Validators.required]
    });
    this.dict.getDictionary('Typ pojazdu')
    .subscribe({
      next: (res=>{
        this.vehicleTypes = res.result.data;
      })
    })
    this.dict.getDictionary('Typ silnika')
    .subscribe({
      next: (res=>{
        this.engineTypes = res.result.data;
      })
    })
    this.dict.getClients()
    .subscribe({
      next: (res=>{
        this.clients = res.result.data;
      })
    })
  }

  addCar(){
    if(this.addCarForm.valid){
      //this.addCarForm.controls['engine'] = [this.engineTypes.find(x=>x.name === this.addCarForm.controls['engine'].value)?.id, true];
      console.log(this.addCarForm.value);
      this.carService.addCar(this.addCarForm.value)
      .subscribe({
        next:(res)=>{
          //console.log(res.result);
          this.addCarForm.reset();
          this.router.navigate(['cars'])
          this.toast.success({detail:"Powodzenie", summary:"Nowy samochód został dodany pomyślnie", duration:3000})
        },
        error:(err)=>{
          this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.addCarForm);
    }
  }
}
