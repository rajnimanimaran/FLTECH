import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';

const API_URL = environment.apiUrl;
@Injectable({
    providedIn: 'root'
})
export class SupplierService {

    constructor(private _http: HttpClient) { }
    insertQuality(data): Observable<any> {
        const dataUrl = API_URL + "Supplier/Create";
        return this._http.post<any>(dataUrl, data)
    }
    updateQuality(data): Observable<any> {
        const dataUrl = API_URL + "Supplier/Modify";
        return this._http.put<any>(dataUrl, data)
    }
    getQuality(): Observable<any> {
        const dataUrl = API_URL + "Supplier/AllSuppliers";
        return this._http.get<any>(dataUrl)
    }

    deleteQualityAudit(Id): Observable<any> {
        const dataUrl = API_URL + "Supplier/Delete/" + Id;
        return this._http.delete<any>(dataUrl, Id)
    }
}
