import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Application, ApplicationStatus } from 'src/app/models/applications.model';
import { Client } from 'src/app/models/clients.model';
import { Dictionary } from 'src/app/models/dictionary.model';
import { User } from 'src/app/models/users.model';
import { AuthService } from 'src/app/services/auth.service';
import { ClientsService } from 'src/app/services/clients.service';
import { DictionaryService } from 'src/app/services/dictionary.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-service-view',
  templateUrl: './service-view.component.html',
  styleUrls: ['./service-view.component.css']
})
export class ServiceViewComponent implements OnInit {
  applications: Application[] = [];
  public fullName : string = "";
  public role : string = "";
  public sid : string = ""; 
  public mail : string ="";
  constructor(private clientsService: ClientsService, private router: Router, private auth: AuthService, private userStore: UserStoreService, private toast: NgToastService, private dict: DictionaryService) {}

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

    this.auth.getApplications().subscribe({
      next: (res=>{
        this.applications = res.result.data;
      }),
      error: (response) => {
        console.log(response);
      }
    })
  }

  changeStatus(application: Application, status: string){
    const obj: ApplicationStatus = {idApplication: application.id, status: status};
    this.auth.changeApplicationStatus(obj)
    .subscribe({
      next:(res)=>{
        this.router.navigate(['zapytania']);
        this.toast.success({detail:"Powodzenie", summary:"Zaktualizowano zapytanie", duration:2000});
      },
      error:(err)=>{
        this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
      }
    })
  }
}
