import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
// import { environment } from 'FT-UI/src/environments/environment';



const API_URL = environment.apiUrl;
//Getmodel:GetID = new GetID();
//model[id] : GetID = [];

@Injectable({
  providedIn: 'root'
})
export class PurchaseOrderService {

  constructor(private _http: HttpClient) { }
  insertPurchaseOrder(data): Observable<any> {
    const dataUrl = API_URL + "PurchaseOrder/Create";
    return this._http.post<any>(dataUrl, data)
  }
  updatePurchaseOrder(data): Observable<any> {
    const dataUrl = API_URL + "PO/Modify";
    return this._http.put<any>(dataUrl, data)
  }

  printPurchase(element): Observable<any> {   
    debugger
    // const dataUrl = API_URL +'Bill/ReportView/'+element.BillNo;
    const dataUrl = API_URL +'PO/ReportView/'+element.PurchaseID;
    return this._http.post<any>(dataUrl,element);
  }
  // deletePurchaseMaster(Getmodel): Observable<any> {
  //   const dataUrl = API_URL + "PurchaseOrder/Delete/" +Getmodel;
  //   return this._http.delete<any>(dataUrl,Getmodel)
  // }
  deletePurchaseDetails(data): Observable<any> {
    debugger;
    const dataUrl = API_URL + "PO/Delete/" + data;
    return this._http.delete<any>(dataUrl, data)
  }
  getPurchaseOrder(PurchaseID,MaterialType): Observable<any> {
    debugger;
    const dataUrl = API_URL + "PurchaseOrder/AllPurchaseOrder/"+PurchaseID+"/"+MaterialType;
    return this._http.get<any>(dataUrl)
  }

  getSupplierName(): Observable<any> {
debugger;    
    const dataUrl = API_URL + "PO/AllSupplierName";
    return this._http.get<any>(dataUrl)
  }
  
  

  getPODetails(id): Observable<any> {
    const dataUrl = API_URL + "PO/GetPoDetails?PurchaseID=" + id;
    return this._http.get<any>(dataUrl)
  }
  getreportPurchaseID(): Observable<any> {
    const dataUrl = API_URL + "PO/getReportpurchaseId";
    return this._http.get<any>(dataUrl)
  }

}
