import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Car } from 'src/app/models/cars.model';
import { Client } from 'src/app/models/clients.model';
import { Dictionary } from 'src/app/models/dictionary.model';
import { User } from 'src/app/models/users.model';
import { AuthService } from 'src/app/services/auth.service';
import { ClientsService } from 'src/app/services/clients.service';
import { DictionaryService } from 'src/app/services/dictionary.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-clients-list',
  templateUrl: './clients-list.component.html',
  styleUrls: ['./clients-list.component.css']
})
export class ClientsListComponent implements OnInit {
  clients: Client[] = [];
  user!: User;
  public fullName : string = "";
  public role : string = "";
  public sid : string = ""; 
  userTypes!: Dictionary[];
  constructor(private clientsService: ClientsService, private router: Router, private auth: AuthService, private userStore: UserStoreService, private toast: NgToastService, private dict: DictionaryService) {}

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

    this.clientsService.getAllClients().subscribe({
      next: (res=>{
        this.clients = res.result.data;
        console.log(this.clients);
      }),
      error: (response) => {
        console.log(response);
      }
    })
  }

  active(client: Client){
    this.clientsService.active(client.id)
    .subscribe({
      next:(res)=>{
        this.toast.success({detail:"Powodzenie", summary:"Zaktualizowano aktywność klienta", duration:2000})
      },
      error:(err)=>{
        this.toast.error({detail:"Błąd", summary:err?.error.message, duration:5000})
      }
    })
  }

  type(client: Client, type: Dictionary){
    this.clientsService.type(client.id, type.id)
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

  addNew(){
    this.router.navigate(['addClient'])
  }

  edit(client: Client){

  }
}
