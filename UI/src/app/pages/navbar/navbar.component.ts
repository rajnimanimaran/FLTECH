import { Component, OnInit, HostBinding } from '@angular/core';
import { Router } from '@angular/router';
import { HeaderStorageService } from 'src/app/shared/services/header-storage.service';
import { OverlayContainer } from '@angular/cdk/overlay';
import { MatDialog } from '@angular/material';
import { NavBarMenuService } from 'src/app/shared/services/nav-bar.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  groupList      : any[] = [];
  getList        : any;
  subGroupList   : any[] = [];
  SubSubGroupList: any[] = [];
  menuList       : any[] = [];
  userName       : string;
  navItems       : NavItem[] = [];
  UserId: any;
  isLoading: boolean;

  constructor(private _NavbarMenuService : NavBarMenuService,private _router:Router,
    private _headerStorage:HeaderStorageService,public _overlayContainer: OverlayContainer,public dialog: MatDialog) {}

  @HostBinding("class")
  componentCssClass;
  onSetTheme(theme) {
    this._overlayContainer.getContainerElement().classList.add(theme);
    this.componentCssClass = theme;
  }

  ngOnInit() {
this.UserId=this._headerStorage.getUserId();
if(this.UserId){
  this._NavbarMenuService.getMenuesForNavbarbyUserid(this.UserId).subscribe((response:any)=>{
debugger;
this.isLoading =true;
    this.navItems=JSON.parse( response._body) 
  })

}else{ 
  
  this._router.navigate[('/login')];
}

  }
}

export interface NavItem {
  Menu_Id    : number;
  displayName: string;
  route?     : string;
  children?  : NavItem[];
}


