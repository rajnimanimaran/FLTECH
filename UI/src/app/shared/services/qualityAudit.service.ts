import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';


const API_URL = environment.apiUrl;
@Injectable({
    providedIn: 'root'
})
export class qualityAuditService {

    constructor(private _http: HttpClient) { }

    getWorkorder(): Observable<any> {
        const dataUrl = API_URL + "WorkOrder/AllWorkOrder/";
        return this._http.get<any>(dataUrl);
    }

    getWorkorderdetail(workorderID){
        const dataUrl = API_URL + "WorkOrder/GetWoDetails";
        return this._http.get<any>(dataUrl,workorderID);
    }

    
}
