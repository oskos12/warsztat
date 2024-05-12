import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from 'src/app/helpers/validateForm';
import { Client } from 'src/app/models/clients.model';
import { Dictionary } from 'src/app/models/dictionary.model';
import { Worker } from 'src/app/models/workers.model';
import { CarsService } from 'src/app/services/cars.service';
import { DictionaryService } from 'src/app/services/dictionary.service';
import { ServicesService } from 'src/app/services/services.service';

@Component({
  selector: 'app-add-service',
  templateUrl: './add-service.component.html',
  styleUrls: ['./add-service.component.css']
})
export class AddServiceComponent implements OnInit {
  addServiceForm!: FormGroup;
  serviceTypes!: Dictionary[];
  workers!: Worker[];
  constructor(private fb: FormBuilder, private serviceService: ServicesService, private router: Router, private toast: NgToastService, private dict: DictionaryService){}

  ngOnInit(): void {
    this.addServiceForm = this.fb.group({
      serviceType: ['', Validators.required],
      count: [],
      hours: [],
      costSum: [],
      description: [''],
      registration: ['', Validators.required],
      supervisor: ['', Validators.required]
    });
    this.dict.getDictionary('Rodzaj usługi')
    .subscribe({
      next: (res=>{
        this.serviceTypes = res.result.data;
      })
    })
    this.dict.getWorkers()
    .subscribe({
      next: (res=>{
        this.workers = res.result.data;
      })
    })
  }

  addService(){
    if(this.addServiceForm.valid){
      console.log(this.addServiceForm.value);
      this.serviceService.addService(this.addServiceForm.value)
      .subscribe({
        next:(res)=>{
          this.addServiceForm.reset();
          this.router.navigate(['services'])
          this.toast.success({detail:"Powodzenie", summary:"Nowy serwis został dodany pomyślnie", duration:3000})
        },
        error:(err)=>{
          this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.addServiceForm);
    }
  }
}
