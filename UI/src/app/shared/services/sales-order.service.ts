import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
// import { environment } from 'FT-UI/src/environments/environment';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class SalesOrderMasterService {

  constructor(private _http: HttpClient) { }
  
  insertSalesOrder(data): Observable<any> {
    debugger;
    const dataUrl = API_URL + "SalesOrder/Create";
    return this._http.post<any>(dataUrl, data)
  }
  updateSalesOrder(data): Observable<any> {
    const dataUrl = API_URL + "SalesOrder/Modify";
    return this._http.put<any>(dataUrl, data)
  }
  getSalesOrder(id){
    const dataUrl = API_URL + "SalesOrder/AllSalesOrder/"+id;
    return this._http.get<any>(dataUrl)
  }
  getSalesOrderCode(){
    const dataUrl = API_URL + "SalesOrder/GetSalesCode/";
    return this._http.get<any>(dataUrl)
  }
  getSalesOrderPlan(id){
    const dataUrl = API_URL + "SalesOrder/GetSalesOrderPlan/"+id;
    return this._http.get<any>(dataUrl)
  }
  saveSOPlanning(data): Observable<any> {
    debugger;
    const dataUrl = API_URL + "SalesOrder/InsertSalesOrderPlan";
    return this._http.post<any>(dataUrl, data)
  }

savesupply(data){
  const dataUrl = API_URL + "SalesOrder/InsertSupplyPlan";
  return this._http.post<any>(dataUrl, data)
}
  
  updateSOPlanning(data): Observable<any> {
    debugger;
    const dataUrl = API_URL + "SalesOrder/UpdateSalesOrderPlan";
    return this._http.post<any>(dataUrl, data)
  }

  // getSalesOrderDetailsbyCode(orderid){
  //   const dataUrl = API_URL + "SalesOrder/GetSalesCode/";
  //   return this._http.post<any>(dataUrl,orderid);
  // }
  // deleteSalesOrder(Id): Observable<any> {
  //   const dataUrl = API_URL + "SalesOrder/Delete/" + Id;
  //   return this._http.delete<any>(dataUrl, Id)
  // }
  // getSalesDetails(): Observable<any> {
  //   const dataUrl = API_URL + "SalesOrder/GetSoDetails";
  //   return this._http.get<any>(dataUrl)
  // }
  getSalesDetails(id): Observable<any> {
    const dataUrl = API_URL + "PO/GetSoDetails?OrderID=" + id;
    return this._http.get<any>(dataUrl)
  }
  printSales(element): Observable<any> {   
    debugger
    // const dataUrl = API_URL +'Bill/ReportView/'+element.BillNo;
    const dataUrl = API_URL +'PO/SalesReportView/'+element.OrderID;
    return this._http.post<any>(dataUrl,element);
  }
  getreportSalesID(): Observable<any> {
    const dataUrl = API_URL + "PO/getReportSalesId";
    return this._http.get<any>(dataUrl)
  }
  deleteSalesDetails(data): Observable<any> {
    debugger;
    const dataUrl = API_URL + "PO/Deletes/" + data;
    return this._http.delete<any>(dataUrl, data)
  }

  
}
