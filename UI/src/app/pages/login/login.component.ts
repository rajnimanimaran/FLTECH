import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { HeaderStorageService } from '../../shared/services/header-storage.service';
import { LoginService } from '../../shared/services/login.service';
import { Router } from '@angular/router';
import { UserAccountService } from '../../shared/services/user-account.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {
  User: any = {};
  userId: any;
  token: any;
  CompanyId: any;


  // ngAfterViewInit() {
  //   (window as any).initialize();
  // }

  // model    : any = { Username: "admin", Password: "admin@123" };
  model:any={};
  // Username:any;
  // Password:any;
  loginForm: FormGroup;
  message  : string;
  returnUrl: string;
  refCompanyId: any;
  CompId: any;
  CompanyName: any;
  isLoading: boolean = true;

  constructor(private formBuilder: FormBuilder,private _headerStorage:HeaderStorageService, private _LoginService :LoginService, 
    private _userService:UserAccountService,  private _router: Router, 
   ) { }
 

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      Username: ['', Validators.required],
      Password: ['', Validators.required]
    });
    this.returnUrl = '/dashboard';
     this.logout();
  }
  
  logout(): void {
    localStorage.setItem('isLoggedIn', "false");
    localStorage.removeItem('token');
  } 
  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  
  login() {
debugger;  
this.isLoading = false  
    //  if ("admin" == this.model.UserName && "admin" == this.model.PassWord) {
      let user: UserLogin = new UserLogin();
      user.Username = this.model.Username;
      user.Password = this.model.Password;
      localStorage.setItem('isLoggedIn', "true");
     
      // localStorage.setItem('token', this.f.UserName.value);
      // this._router.navigate([this.returnUrl]);
      // this._router.navigate(['View']);
   this.loginservice(user);
    // }

    // else{
    //  alert("UserName & PassWord InCorrect")
    //    this.message = "Please check your UserName and passWord";
    //    }
    }    

loginservice(user) { 
  debugger;
  this._LoginService.login(user).subscribe((response:any) => {
if(response){
  this.isLoading = true;
    this.userId = response.headers.get("UserId");
    this.CompanyId=response.headers.get("CompanyId")
    this.token = response.headers.get("Token");
    if( this.userId!=undefined || this.userId!=null && this.token!=undefined || this.token!=null ){
      this._headerStorage.setUserId(this.userId);
      this._headerStorage.setToken(this.token);
      this.getCompanyIdByUserId();
      this._router.navigate([this.returnUrl])
    }
  }
  // else{
  //   setTimeout((response)=>{ this.isLoading = true;},5000)
  //   alert('cannot reach')
  // }
  });
  
}

getCompanyIdByUserId(){
  debugger;
 this.refCompanyId =this._headerStorage.getUserId()
//  this._userService.companyIdByUserId(this.refCompanyId).subscribe((data:any)=>
//  {
//  if(data){
// this.CompId=JSON.parse(data._body);
// console.log(this.CompId,"all details")
// this.CompanyId=this.CompId.Company.CompanyId;
// this.CompanyName=this.CompId.Company;
// this._headerStorage.setCompanyId(this.CompanyId)
// this._headerStorage.setCompanyName(this.CompanyName)
// console.log(this.CompanyName,"CompanyID")
//  }})
}




}

export class UserLogin {
  User_Id: number;
  Username: string;
  Password: string;
  Sales_Location_Id: number;

}