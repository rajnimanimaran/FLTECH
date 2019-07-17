import { Injectable } from '@angular/core';

@Injectable()
export class HeaderStorageService {
  UserId: any;
  token: any;
  CompanyId: number;
  CompanyName: number;
  

  constructor() { }
setUserId(value:any){
  this.UserId=value
}
// StereoPannerOptions
getUserId(){
  return this.UserId;
}

setToken(token:string){
  this.token=token;
}

getToken(){
  debugger;
  return this.token;
}
setCompanyId(value:number){
  this.CompanyId=value;
}

getCompanyId(){
  debugger;
  return this.CompanyId;
}


setCompanyName(value:number){
  this.CompanyName=value;
}

getCompanyName(){
  debugger;
  return this.CompanyName;
}

}
