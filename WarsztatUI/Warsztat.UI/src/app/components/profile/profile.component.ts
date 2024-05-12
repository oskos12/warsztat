import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from 'src/app/helpers/validateForm';
import { Client } from 'src/app/models/clients.model';
import { Result } from 'src/app/models/result.model';
import { AuthService } from 'src/app/services/auth.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  clientForm!: FormGroup;
  sid!: string;
  fullName!: string;
  client: string[] = [];
  edit: boolean = false;
  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router, private toast: NgToastService, private userStore: UserStoreService){}

  ngOnInit(): void {
    this.userStore.getSidFromStorage()
    .subscribe(val=>{
      const sidFromToken = this.auth.getSidFromToken();
      this.sid = val || sidFromToken;
    })

    this.userStore.getFullNameFromStorage()
    .subscribe(val=>{
      const fullNameFromToken = this.auth.getFullNameFromToken();
      this.fullName = val || fullNameFromToken;
    })

    this.auth.getClientDetails(this.sid)
    .subscribe({
      next:(res)=>{
        this.client = res.result.data;
        console.log(Object.values(res.result.data))
        this.client = Object.values(res.result.data);
      }
    })

    this.clientForm = this.fb.group({
      id: [this.sid],
      city: ['', Validators.required],
      post: ['', Validators.required],
      address: ['', Validators.required],
      phoneNumber: ['', Validators.required]
    })
  }
  editData(){
    this.edit = true;
    this.auth.getClientDetails(this.sid)
    .subscribe({
      next:(res)=>{
        this.client = res.result.data;
        console.log(Object.values(res.result.data))
        this.client = Object.values(res.result.data);
      }
    })
  }
  back(){
    this.edit = false;
  }
  onSubmit(){
    if(this.clientForm.valid){
      this.auth.setClientDetails(this.clientForm.value)
      .subscribe({
        next:(res)=>{
          this.clientForm.reset();
          this.toast.success({detail:"Powodzenie", summary:"Dane zostały zmienione", duration:3000})
          this.edit = false;
          window.location.reload();
        },
        error:(err)=>{
          this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.clientForm);
    }
  }
}
