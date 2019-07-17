import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, from } from 'rxjs';
import { environment } from '../../../../environments/environment';


const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class BOMMasterService {
  constructor(private _http: HttpClient) { }
  insertBOMMaster(data):Observable<any>{
    debugger;
    const dataUrl = API_URL +"BOM_master/Create";
    return this._http.post<any>(dataUrl,data)
  }
  updateBOMMaster(data):Observable<any>{
    debugger;
    const dataUrl = API_URL + "BOM_master/Modify";
    return this._http.put<any>(dataUrl,data)
  }
  deleteBOMMaster(Id):Observable<any>{
  const dataUrl = API_URL + "BOM_master/Delete/"+Id;
    return this._http.delete<any>(dataUrl,Id)
  }
  getBOMMaster(BOMID):Observable<any>{
    const dataUrl = API_URL + "BOM_master/AllBOM_master/"+BOMID;
    return this._http.get<any>(dataUrl)
  }

  
}
