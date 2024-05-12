import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Response } from '../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class WorkersService {
  baseApiUrl: string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }

  getAllWorkers(): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'workersUser');
  }
  active(id: number): Observable<Response>{
    return this.http.put<Response>(this.baseApiUrl + 'activeWorker', id)
  }
  type(id: number, typeId: number): Observable<Response>{
    return this.http.put<Response>(this.baseApiUrl + 'typeUser?id=' + id, typeId)
  }
  getUser(id: number): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'user?id=' + id)
  }
}