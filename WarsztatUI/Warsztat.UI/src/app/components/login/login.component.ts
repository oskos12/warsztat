import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from 'src/app/helpers/validateForm';
import { AuthService } from 'src/app/services/auth.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash"
  loginForm!: FormGroup;

  constructor(private fb: FormBuilder, private auth: AuthService, private toast: NgToastService, private router:Router, private userStore: UserStoreService){}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      login: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  hideShowPassword(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }

  onSubmit(){
    if(this.loginForm.valid){
      this.auth.login(this.loginForm.value)
      .subscribe({
        next:(res)=>{
          if(res.statusCode === 404)
            this.toast.error({detail:"Błąd", summary:"Niepoprawne dane logowania", duration:3000})
          else{
            console.log(res.result.data);
            this.auth.storeToken(res.result.data.toString());
            const toketPayload = this.auth.decodedToken();
            this.userStore.setFullNameForStorage(toketPayload.name);
            this.userStore.setRoleForStorage(toketPayload.role);
            this.userStore.setSidForStorage(toketPayload.sid);
            this.toast.success({detail:"Powodzenie", summary:"Zalogowano pomyślnie", duration:3000})
            this.router.navigate([''])
          }
        },
        error:(err)=>{
          this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.loginForm);
    }
  }
}
