import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from 'src/app/helpers/validateForm';
import { Client } from 'src/app/models/clients.model';
import { Dictionary } from 'src/app/models/dictionary.model';
import { CarsService } from 'src/app/services/cars.service';
import { ClientsService } from 'src/app/services/clients.service';
import { DictionaryService } from 'src/app/services/dictionary.service';

@Component({
  selector: 'app-add-client',
  templateUrl: './add-client.component.html',
  styleUrls: ['./add-client.component.css']
})
export class AddClientComponent implements OnInit {
  addClientForm!: FormGroup;
  constructor(private fb: FormBuilder, private clientService: ClientsService, private router: Router, private toast: NgToastService, private dict: DictionaryService){}

  ngOnInit(): void {
    this.addClientForm = this.fb.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      city: [''],
      post: [''],
      address: [''],
      email: ['', Validators.required]
    });
  }

  addClient(){
    if(this.addClientForm.valid){
      this.clientService.addNewClient(this.addClientForm.value)
      .subscribe({
        next:(res)=>{
          this.addClientForm.reset();
          this.router.navigate(['clients'])
          this.toast.success({detail:"Powodzenie", summary:"Nowy klient został dodany pomyślnie", duration:3000})
        },
        error:(err)=>{
          this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.addClientForm);
    }
  }
}
