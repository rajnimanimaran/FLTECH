import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment'


const API_URL = environment.apiUrl;
@Injectable({
    providedIn: 'root'
})
export class CustomerService {

    constructor(private _http: HttpClient) { }

    insertCustomer(data): Observable<any> {
        const dataUrl = API_URL + "Customer/Create";
        return this._http.post<any>(dataUrl, data)
    }
    updateCustomer(data): Observable<any> {
        const dataUrl = API_URL + "Customer/Modify";
        return this._http.put<any>(dataUrl, data)
    }
    deleteCustomer(Id): Observable<any> {
        const dataUrl = API_URL + "Customer/Delete/" + Id;
        return this._http.delete<any>(dataUrl, Id)
    }
    getCustomer(): Observable<any> {
        const dataUrl = API_URL + "Customer/AllCustomer";
        return this._http.get<any>(dataUrl)
    }
    getState(id): Observable<any> {
        debugger;
        const dataUrl = API_URL + "State/GetStateByCountryId?id=" + id;
        return this._http.get<any>(dataUrl)
    }
    getCity(id): Observable<any> {
        debugger;
        const dataUrl = API_URL + "City/GetCityByStateId?StateId=" + id;
        return this._http.get<any>(dataUrl)
    }
    getCustMaster(): Observable<any> {
        const dataUrl = API_URL + "SalesOrderMaster/GetCustomerName";
        return this._http.get<any>(dataUrl)
    }
}
