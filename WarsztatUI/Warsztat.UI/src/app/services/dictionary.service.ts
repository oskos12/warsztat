import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Response } from '../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class DictionaryService {
  baseApiUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getDictionary(type: string): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'dictionary/?name=' + type);
  }

  getClients(): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'clients')
  }

  getWorkers(): Observable<Response>{
    return this.http.get<Response>(this.baseApiUrl + 'workers')
  }
}
