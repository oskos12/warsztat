import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import {HttpClient, HttpResponse, HttpResponseBase} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Car } from '../models/cars.model';
import { Response } from '../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class CarsService {
  baseApiUrl: string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }

  getAllCars(): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'cars');
  }
  addCar(carObj: any): Observable<Response>{
    console.log(carObj);
    return this.http.post<Response>(this.baseApiUrl + 'addCar', carObj)
  }
  getCars(id: any): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'getCars/?userId=' + id);
  }
  active(id: number): Observable<Response>{
    return this.http.put<Response>(this.baseApiUrl + 'activeCar', id)
  }
  type(id: number, typeId: number): Observable<Response>{
    return this.http.put<Response>(this.baseApiUrl + 'typeCar?id=' + id, typeId)
  }
  getServices(id: any): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'carServices/?carId=' + id);
  }
}
