import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
// import { environment } from 'FT-UI/src/environments/environment';
const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class WorkorderService {

  constructor(private _http: HttpClient) { }
  insertWorkOrder(data): Observable<any> {
    const dataUrl = API_URL + "WorkOrder/CreatePO";
    return this._http.post<any>(dataUrl, data)
  }
}
