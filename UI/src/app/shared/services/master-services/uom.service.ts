import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_URL = environment.apiUrl;

@Injectable({
    providedIn: 'root'
})
export class UOMService {

    constructor(private _http: HttpClient) { }

    insert(data): Observable<any> {
        const dataUrl = API_URL + "UOMMaster/Create";
        return this._http.post<any>(dataUrl, data)
    }
    update(data): Observable<any> {
        const dataUrl = API_URL + "UOMMaster/Modify";
        return this._http.put<any>(dataUrl, data)
    }
    delete(Id): Observable<any> {
        const dataUrl = API_URL + "UOMMaster/Delete/" + Id;
        return this._http.delete<any>(dataUrl, Id)
    }
    get(): Observable<any> {
        const dataUrl = API_URL + "UOMMaster/AllUOM";
        return this._http.get<any>(dataUrl)
    }

}

