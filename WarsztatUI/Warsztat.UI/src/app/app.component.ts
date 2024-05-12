import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { UserStoreService } from './services/user-store.service';
import { Client } from './models/clients.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  public fullName : string = "";
  public role : string = "";
  public sid : string = "";
  constructor(private auth: AuthService, private userStore: UserStoreService,  private router: Router){

  }
  title = 'Warsztat';

  ngOnInit(){
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

  }

  logout(){
    this.fullName = "";
    this.role = "";
    this.sid = "";
    this.auth.signout();
  }

  userData(){
    this.router.navigate(['profil'])
  }
}
