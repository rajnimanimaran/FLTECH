import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_URL = environment.apiUrl;

@Injectable({
    providedIn: 'root'
})
export class ProductService {

    constructor(private _http: HttpClient) { }
    insertProduct(data): Observable<any> {
        const dataUrl = API_URL + "ProductMaster/Create";
        return this._http.post<any>(dataUrl, data)
    }
    updateProduct(data): Observable<any> {
        const dataUrl = API_URL + "ProductMaster/Modify";
        return this._http.put<any>(dataUrl, data)
    }
    deleteProduct(Id): Observable<any> {
        const dataUrl = API_URL + "ProductMaster/Delete/" + Id;
        return this._http.delete<any>(dataUrl, Id)
    }
    getProduct(): Observable<any> {
        const dataUrl = API_URL + "ProductMaster/Allproduct";
        return this._http.get<any>(dataUrl)
    }
    getUOMMaster(): Observable<any> {
        const dataUrl = API_URL + "Product/GetUOMMaster";
        return this._http.get<any>(dataUrl)
    }
    getproductMaster(): Observable<any> {
        const dataUrl = API_URL + "ProductMaster/GetproductMaster";
        return this._http.get<any>(dataUrl)
    }
}
