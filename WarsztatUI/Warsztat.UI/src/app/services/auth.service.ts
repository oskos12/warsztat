import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Response } from '../models/response.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { isThisTypeNode } from 'typescript';
import { Client } from '../models/clients.model';
import { ApplicationStatus } from '../models/applications.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseApiUrl: string = environment.baseApiUrl;
  logoutVisible: boolean = true;
  private userPayload:any;
  constructor(private http: HttpClient, private router: Router, private toast: NgToastService) {
    this.userPayload = this.decodedToken();
   }

  getAllUsers(): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'users');
  }

  signup(userObj:any): Observable<Response>{
    return this.http.post<Response>(this.baseApiUrl + 'signup', userObj)
  }

  login(loginObj:any): Observable<Response>{
    return this.http.post<Response>(this.baseApiUrl + 'login', loginObj)
  }

  signout(){
    localStorage.clear();
    this.router.navigate(['login']);
    this.toast.success({detail:"Powodzenie", summary:"Wylogowano pomyślnie", duration:3000})
  }

  storeToken(tokenValue: string){
    localStorage.setItem('token', tokenValue)
  }

  getToken(){
    return localStorage.getItem('token')
  }

  isLoggedIn():boolean{
    return !!localStorage.getItem('token')
  }

  decodedToken(){
    const jwtHelper = new JwtHelperService();
    return jwtHelper.decodeToken(this.getToken()!);
  }

  getFullNameFromToken(){
    if(this.userPayload)
      return this.userPayload.name;
    else
      return '';
  }

  getRoleFromToken(){
    if(this.userPayload)
      return this.userPayload.role;
    else
      return '';
  }

  getSidFromToken(){
    if(this.userPayload)
      return this.userPayload.sid;
    else
      return '';
  }

  offerQuery(queryObj:any): Observable<Response>{
    return this.http.post<Response>(this.baseApiUrl + 'offerQuery', queryObj)
  }

  getClientDetails(id: string): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'client?userId=' + id)
  }

  setClientDetails(clientObj:any): Observable<Response>{
    return this.http.put<Response>(this.baseApiUrl + 'client', clientObj)
  }

  getApplications(): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'applications')
  }

  changeApplicationStatus(obj: ApplicationStatus){
    return this.http.put<Response>(this.baseApiUrl + 'applications', obj )
  }

  getClientByIdDetails(id: number): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'client?userId=' + id)
  }
}
