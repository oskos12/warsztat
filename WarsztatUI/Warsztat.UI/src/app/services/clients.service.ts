import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Response } from '../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class ClientsService {
  baseApiUrl: string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }

  getAllClients(): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'clientsUser');
  }
  addClient(clientObj: any): Observable<Response>{
    return this.http.put<Response>(this.baseApiUrl + 'client', clientObj)
  }
  addNewClient(clientObj: any): Observable<Response>{
    return this.http.post<Response>(this.baseApiUrl + 'client', clientObj)
  }
  active(id: number): Observable<Response>{
    return this.http.put<Response>(this.baseApiUrl + 'activeClient', id)
  }
  type(id: number, typeId: number): Observable<Response>{
    return this.http.put<Response>(this.baseApiUrl + 'typeUser?id=' + id, typeId)
  }
  getUser(id: number): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'user?id=' + id)
  }
}
