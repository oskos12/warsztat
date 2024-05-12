import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from 'src/app/helpers/validateForm';
import { AuthService } from 'src/app/services/auth.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-service',
  templateUrl: './service.component.html',
  styleUrls: ['./service.component.css']
})
export class ServiceComponent {
  queryForm!: FormGroup;
  public role : string = "";
  public sid : string = "";
  constructor(private fb: FormBuilder, private auth: AuthService, private toast: NgToastService, private router: Router, private userStore: UserStoreService){}

  ngOnInit(): void {
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

    if(this.role === ''){
      this.queryForm = this.fb.group({
        phoneNumber: ['', Validators.required],
        email: ['', Validators.required],
        clientId: [this.sid],
        query: ['', Validators.required]
      })
    }
    else{
      this.queryForm = this.fb.group({
        phoneNumber: [''],
        email: [''],
        clientId: [this.sid],
        query: ['', Validators.required]
      })
    }
  }
  
  onSubmit(){
    if(this.queryForm.valid){
      this.auth.offerQuery(this.queryForm.value)
      .subscribe({
        next:(res)=>{
          this.toast.success({detail:"Powodzenie", summary:"Pomyślnie przesłano zapytanie", duration:3000})
          this.router.navigate([''])
        },
        error:(err)=>{
          this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.queryForm);
    }
  }
}

