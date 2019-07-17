import { Injectable, ApplicationInitStatus } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'  
import 'rxjs/Rx';   
import { environment } from '../../../../../environments/environment';
import { HeaderStorageService } from '../../header-storage.service';
import { Http, Headers } from '@angular/http';
import { CountryEntity } from 'src/app/shared/models/location/country';

const API_URL = environment.apiUrl;

@Injectable()
export class CountryService {
  CountryValue: any = [];
  isEditing: boolean;

  constructor(private _http: Http,private _headerStorage:HeaderStorageService) { }
setIsEditing(value:boolean){
this.isEditing=value
}
getIsEditing(){
  return this.isEditing;
  }
  listCountryDetails():Observable<any>{
    debugger;
    const headers = new Headers();
    headers.append('Token', this._headerStorage.getToken());
  const dataUrl = API_URL+'Country/AllCountry';
  return this._http.get(dataUrl, {headers:headers})
  }

  saveCountryDetail(element){
    const headers = new Headers();
    headers.append('Token', this._headerStorage.getToken());
    return this._http.post(API_URL+'Country/Create', element,{headers:headers}).map(response => {
      return response;
    }).catch(
      this.handleError
    );
  }
  
  updateCountryDetail(element){
    debugger;
    const headers = new Headers();
    headers.append('Token', this._headerStorage.getToken());
    return this._http.put(API_URL+'Country/Modify', element,{headers:headers}).map(response => {
      return response;
    }).catch(
      this.handleError
    );
  }

  deleteCountryDetail(id){
    debugger;
    const headers = new Headers();
    headers.append('Token', this._headerStorage.getToken());
    const country           = new CountryEntity
          country.CountryId = id
    const dataUrl           = API_URL+'Country/Delete/'+id.CountryId;
    return this._http.delete(dataUrl,{headers:headers}).map(response => {
      return response;
    }).catch(
      this.handleError
    );
  }
  GetCountryList():Observable<any>{
    const headers = new Headers();
    headers.append('Token', this._headerStorage.getToken());
      const dataUrl = API_URL +'Country/AllCountry';
      return this._http.get(dataUrl,{headers:headers});
  }

  handleError(error: Response) {
    return Observable.throw(error);
  }

setCountryVal(element:any=[]){
  debugger
  this.CountryValue = element
}
getCountryVal(){
  debugger
  return this.CountryValue;
}
}
