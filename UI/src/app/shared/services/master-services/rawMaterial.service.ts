import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';

const API_URL = environment.apiUrl;

@Injectable({
    providedIn: 'root'
})
export class RawMaterialService {

    constructor(private _http: HttpClient) { }
    insertRawmaterial(data): Observable<any> {
        debugger;
        const dataUrl = API_URL + "RawMaterialmaster/Create";
        return this._http.post<any>(dataUrl, data)
    }
    updateRawmaterial(data): Observable<any> {
        debugger;
        const dataUrl = API_URL + "RawMaterialmaster/Modify";
        return this._http.put<any>(dataUrl, data)
    }
    deleteRawmaterial(Id): Observable<any> {
        const dataUrl = API_URL + "RawMaterialmaster/Delete/" + Id;
        return this._http.delete<any>(dataUrl, Id)
    }
    getRawmaterial(): Observable<any> {
        const dataUrl = API_URL + "RawMaterialmaster/AllRawmaterial";
        return this._http.get<any>(dataUrl)
    }

}
