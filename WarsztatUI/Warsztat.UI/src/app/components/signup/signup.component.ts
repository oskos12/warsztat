import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from 'src/app/helpers/validateForm';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash"
  signupForm!: FormGroup;

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router, private toast: NgToastService){}

  ngOnInit(): void {
    this.signupForm = this.fb.group({
      login: ['', Validators.required],
      password: ['', Validators.required],
      email: ['', Validators.required],
      name: ['', Validators.required],
      surname: ['', Validators.required],
      phoneNumber: ['', Validators.required]
    })
  }

  hideShowPassword(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }

  onSubmit(){
    if(this.signupForm.valid){
      this.auth.signup(this.signupForm.value)
      .subscribe({
        next:(res)=>{
          if(res.statusCode === 400)
            this.toast.error({detail:"Błąd", summary:"Podany login lub email jest już zajety", duration:3000})
          else{
            this.signupForm.reset();
            this.toast.success({detail:"Powodzenie", summary:"Nowe konto utworzone pomyślnie", duration:3000})
            this.router.navigate(['login'])
          }
        },
        error:(err)=>{
          this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.signupForm);
    }
  }
}
