import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Response } from '../models/response.model';
import { WorkerService } from '../models/service.model';

@Injectable({
  providedIn: 'root'
})
export class ServicesService {
  baseApiUrl: string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }

  getAllServices(): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'services');
  }
  addService(serviceObj: any): Observable<Response>{
    console.log(serviceObj);
    return this.http.post<Response>(this.baseApiUrl + 'addService', serviceObj)
  }
  getServices(id: any): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'getServices/?userId=' + id);
  }
  getWorkers(): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'workers');
  }
  addWorkers(obj: WorkerService): Observable<Response>{
    return this.http.post<Response>(this.baseApiUrl + 'addWorkers', obj)
  }
  finishService(id: number): Observable<Response>{
    return this.http.put<Response>(this.baseApiUrl + 'finishService', id)
  }
}
