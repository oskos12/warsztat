import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Client } from 'src/app/models/clients.model';
import { Dictionary } from 'src/app/models/dictionary.model';
import { User } from 'src/app/models/users.model';
import { Worker } from 'src/app/models/workers.model';
import { AuthService } from 'src/app/services/auth.service';
import { ClientsService } from 'src/app/services/clients.service';
import { DictionaryService } from 'src/app/services/dictionary.service';
import { UserStoreService } from 'src/app/services/user-store.service';
import { WorkersService } from 'src/app/services/workers.service';

@Component({
  selector: 'app-workers',
  templateUrl: './workers.component.html',
  styleUrls: ['./workers.component.css']
})
export class WorkersComponent implements OnInit {
  workers: Worker[] = [];
  user!: User;
  public fullName : string = "";
  public role : string = "";
  public sid : string = ""; 
  userTypes!: Dictionary[];
  constructor(private workersService: WorkersService, private router: Router, private auth: AuthService, private userStore: UserStoreService, private toast: NgToastService, private dict: DictionaryService) {}

  ngOnInit(): void {
    this.dict.getDictionary('Typ konta')
    .subscribe({
      next: (res=>{
        this.userTypes = res.result.data;
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

    this.workersService.getAllWorkers().subscribe({
      next: (res=>{
        this.workers = res.result.data;
        console.log(this.workers);
      }),
      error: (response) => {
        console.log(response);
      }
    })
  }

  active(worker: Worker){
    this.workersService.active(worker.id)
    .subscribe({
      next:(res)=>{
        this.toast.success({detail:"Powodzenie", summary:"Zaktualizowano aktywność klienta", duration:2000})
      },
      error:(err)=>{
        this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
      }
    })
  }

  type(worker: Worker, type: Dictionary){
    this.workersService.type(worker.id, type.id)
    .subscribe({
      next:(res)=>{
        if(type.value === "User")
          this.router.navigate(['clients']);
        this.toast.success({detail:"Powodzenie", summary:"Zaktualizowano typ konta", duration:2000})
      },
      error:(err)=>{
        this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
      }
    })
  }
  edit(worker: Worker){

  }
}

