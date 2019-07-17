import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
// import { environment } from 'FT-UI/src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class QualitycontrolService {
  // QCDetails(): any {
  //   throw new Error("Method not implemented.");
  // }

  constructor(private _http: HttpClient) { }
  insertQuality(data): Observable<any> {
    const dataUrl = API_URL + "QCCheck/Create";
    return this._http.post<any>(dataUrl, data)
  }
  updateQuality(data): Observable<any> {
    const dataUrl = API_URL +"QCCheck/Modify";
    return this._http.put<any>(dataUrl, data)
  }
  deleteQuality(data): Observable<any> {
    const dataUrl = API_URL + "QCCheck/Delete/" + data;
    return this._http.delete<any>(dataUrl, data)
  }
  getQuality(): Observable<any> {
    const dataUrl = API_URL + "QCCheck/AllQCCheck";
    return this._http.get<any>(dataUrl)
  }

  getQCList(Id): Observable<any> {
    const dataUrl = API_URL + "QCCheck/GetQCCheckDetails/" + Id;
    return this._http.get<any>(dataUrl)
  }
  

}
