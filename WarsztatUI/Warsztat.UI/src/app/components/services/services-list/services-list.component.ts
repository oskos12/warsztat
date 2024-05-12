import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from 'src/app/helpers/validateForm';
import { Service, ServicesWithWorkers, WorkerService } from 'src/app/models/service.model';
import { Worker } from 'src/app/models/workers.model';
import { AuthService } from 'src/app/services/auth.service';
import { ServicesService } from 'src/app/services/services.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-services-list',
  templateUrl: './services-list.component.html',
  styleUrls: ['./services-list.component.css']
})
export class ServicesListComponent implements OnInit {
  addWorkersForm!: FormGroup;
  services: ServicesWithWorkers[] = [];
  workers: Worker[] = [];
  public fullName : string = "";
  public role : string = "";
  public sid : string = ""; 
  public awControl:boolean = false;
  history: boolean = false;
  constructor(private servicessService: ServicesService, private router: Router, private auth: AuthService, private userStore: UserStoreService, private fb: FormBuilder, private toast: NgToastService) {}

  ngOnInit(): void {
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
      this.servicessService.getAllServices()
      .subscribe({
        next: (res=>{
          this.services = res.result.data;
        }),
        error: (response) => {
          console.log(response);
        }
      })
    }
    else{
      this.servicessService.getServices(this.sid).subscribe({
        next: (res=>{
          if(res.statusCode != 404)
            this.services = res.result.data;
        }),
        error: (response) => {
          console.log(response);
        }
      })
    }
    
  }

  viewHistory(){
    this.history = true;
  }
  back(){
    this.history = false;
  }
  addNew(){
    this.router.navigate(['addService'])
  }
  edit(car: Service){
    
  }
  getWorkers(){
    this.awControl = true;
    this.servicessService.getWorkers()
    .subscribe({
      next: (res=>{
        this.workers = res.result.data;
      }),
      error: (response) => {
        console.log(response);
      }
    })
    this.addWorkersForm = this.fb.group({
      newWorkers: ['', Validators.required]
    });
  }
  addWorkers(id: number){
    if(this.addWorkersForm.valid){
      const obj: WorkerService = {id: id, workers: this.addWorkersForm.value};
      this.servicessService.addWorkers(obj)
      .subscribe({
        next:(res)=>{
          this.addWorkersForm.reset();
          this.awControl = false;
          //this.router.navigate(['services'])
          window.location.reload();
          this.toast.success({detail:"Powodzenie", summary:"Przypisano pracowników do serwisu", duration:3000})
        },
        error:(err)=>{
          this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.addWorkersForm);
    }
  }

  finish(service: Service){
    this.servicessService.finishService(service.id)
    .subscribe({
      next:()=>{
        //this.router.navigate(['services'])
        window.location.reload();
        this.toast.success({detail:"Powodzenie", summary:"Zakończono serwis "+service.dictDescription, duration:2000})
      },
      error:(err)=>{
        this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
      }
    })
  }

  toPdf(){
    let doc = new jsPDF({orientation: "landscape"});
    autoTable(doc, {html: '#table'})
    doc.save("raport.pdf");
  }

}
