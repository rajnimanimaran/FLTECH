import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


const API_URL = environment.apiUrl;

@Injectable({
    providedIn: 'root'
})
export class SubContractService {

    constructor(private _http: HttpClient) { }
    insertsubcontract(data): Observable<any> {
        const dataUrl = API_URL + "SubContract/Create";
        return this._http.post<any>(dataUrl, data)
    }
    updatesubcontract(data): Observable<any> {
        const dataUrl = API_URL + "SubContract/Modify";
        return this._http.put<any>(dataUrl, data)
    }
    deletesubcontract(Id): Observable<any> {
        const dataUrl = API_URL + "SubContract/Delete/" + Id;
        return this._http.delete<any>(dataUrl, Id)
    }
    getsubContract(): Observable<any> {
        const dataUrl = API_URL + "SubContract/Allcontract";
        return this._http.get<any>(dataUrl)
    }
}
