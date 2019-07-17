import { Injectable } from '@angular/core';
import { Http, Headers } from "@angular/http";
import { Observable } from "rxjs/Rx";
import { HeaderStorageService } from './header-storage.service';
import { environment } from 'src/environments/environment';
// import 'rxjs/add/operator/map';
// import 'rxjs/add/operator/catch';

const API_URL = environment.apiUrl;

@Injectable()
export class NavBarMenuService {

  constructor(private _headerStorage:HeaderStorageService,private _http: Http) { }

  getMenuesForNavbarbyUserid(element): Observable<any>{
    debugger;
    const headers = new Headers();
    headers.append('Token', this._headerStorage.getToken());
    return this._http.get(API_URL + 'UserMenuMapping/GetMenuMappingByUserId/'+ element, { headers: headers }).map(response => {
      return response;
     }).catch(
        this.handleError);
    }
      handleError(error: Response) {
        return Observable.throw(error);
      }
  }
  