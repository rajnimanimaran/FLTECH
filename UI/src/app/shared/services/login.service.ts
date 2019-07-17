import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';

const API_URL = environment.apiUrl;
@Injectable()
export class LoginService {

  constructor(private _http:HttpClient) { }



login(element) {
  debugger;
  return this._http.post<any>(API_URL + 'login', element,
      {
          headers: new HttpHeaders().set('Authorization', 'Basic ' + btoa(element.Username + ":" + element.Password)), 
          observe: 'response'
      });
}
}
