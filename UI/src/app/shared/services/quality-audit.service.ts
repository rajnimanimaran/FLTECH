import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';


const API_URL = environment.apiUrl;
@Injectable({
    providedIn: 'root'
})
export class QualityAuditService {

    constructor(private _http: HttpClient) { }

    getWorkorder(id): Observable<any> {
        const dataUrl = API_URL + "WorkOrder/GetWosales/"+id;
        return this._http.get<any>(dataUrl);
    }

    getWorkorderdetail(workorderID){
        const dataUrl = API_URL + "WorkOrder/WOSdetails/"+workorderID;
        return this._http.get<any>(dataUrl);
    }
    getQCdetail(prdID){
        const dataUrl = API_URL + "WorkOrder/QCdetails/"+prdID;
        return this._http.get<any>(dataUrl);
    }

}
