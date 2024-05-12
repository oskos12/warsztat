import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserStoreService {
  private fullName$ = new BehaviorSubject<string>("");
  private role$ = new BehaviorSubject<string>("");
  private sid$ = new BehaviorSubject<string>("");

  constructor() { }

  public getRoleFromStorage(){
    return this.role$.asObservable();
  }

  public setRoleForStorage(role:string){
    this.role$.next(role);
  }

  public getFullNameFromStorage(){
    return this.fullName$.asObservable();
  }

  public setFullNameForStorage(fullName:string){
    this.fullName$.next(fullName);
  }

  public getSidFromStorage(){
    return this.sid$.asObservable();
  }

  public setSidForStorage(sid:string){
    this.sid$.next(sid);
  }
}
