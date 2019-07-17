import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialModule } from './shared/common/material/material.module';
import { HttpClientModule } from '@angular/common/http';
// import { SatPopoverModule } from '@ncstate/sat-popover';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { MAT_DATE_LOCALE, DateAdapter, MAT_DATE_FORMATS } from '@angular/material';
import { AppDateAdapter, APP_DATE_FORMATS } from './shared/directives/app-date-adapter.directive';
import { UomMasterComponent } from './pages/master/uom-master/uom-master.component';
import { LoginComponent } from './pages/login/login.component';
import { LoginService } from './shared/services/login.service';
import { UserAccountService } from './shared/services/user-account.service';
import { HeaderStorageService } from './shared/services/header-storage.service';
// import { NavbarComponentComponent } from './navbar-component/navbar-component.component';
import { NavbarComponent } from './pages/navbar/navbar.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AuthGuard } from './shared/directives/authGuard';
import { NavMenuItemsComponent } from './pages/nav-menu-items/nav-menu-items.component';
import { NavBarMenuService } from './shared/services/nav-bar.service';
import { FlowTechDialogComponent } from './pages/Dialog/flow-tech-dialog/flow-tech-dialog.component';
import { FlexLayoutModule } from "@angular/flex-layout";
import { RawMaterialMasterComponent } from './pages/master/raw-material-master/raw-material-master.component';
import { ProductMasterComponent } from './pages/master/product-master/product-master.component';
import { CustomerMasterComponent } from './pages/master/customer-master/customer-master.component';
import { ContractorMasterComponent } from './pages/master/contractor-master/contractor-master.component';
import { SupplierMasterComponent } from './pages/master/supplier-master/supplier-master.component';
import { BOMComponent } from './pages/bom/bom.component';
import { UOMService } from './shared/services/master-services/uom.service';
import { ProductService } from './shared/services/master-services/product.service';
import { BOMMasterService } from './shared/services/master-services/bom.service';
import { CustomerService } from './shared/services/master-services/customer.service';
import { SubContractService } from './shared/services/master-services/contractor.service';
import { RawMaterialService } from './shared/services/master-services/rawMaterial.service';
import { SupplierService } from './shared/services/master-services/supplier.service';
// import { AreaServicesService } from './shared/services/adminstrator/location/area.service';
// import { CityServicesService } from './shared/services/adminstrator/location/city.service';
 import { CountryService } from './shared/services/adminstrator/location/country.service';
import { PurchaseOrderComponent } from './pages/purchase-order/purchase-order.component';
import { SalesOrderComponent } from './pages/sales-order/sales-order.component';
import { PurchaseOrderService } from './shared/services/purchase-order.service';
import { SalesOrderMasterService } from './shared/services/sales-order.service';
import { WoInhouseComponent } from './pages/Workorder master/wo-inhouse/wo-inhouse.component';
import { WoJobworkComponent } from './pages/Workorder master/wo-jobwork/wo-jobwork.component';
import { QualityControlComponent } from './pages/master/quality-control/quality-control.component';
import { SpinnerComponent } from './pages/Dialog/spinner/spinner.component';
import { WorkorderService } from './shared/services/workorder.service';
import { QualityAuditComponent } from './pages/quality-audit/quality-audit.component';
import { QualityAuditService } from './shared/services/quality-audit.service';
import { PlanningComponent } from './pages/planning/planning.component';
// import { StateServicesService } from './shared/services/adminstrator/location/state.service';
@NgModule({
  declarations: [
    AppComponent,
    UomMasterComponent,
    LoginComponent,
    NavbarComponent,
    DashboardComponent,
    NavMenuItemsComponent,
    FlowTechDialogComponent,
    RawMaterialMasterComponent,
    ProductMasterComponent,
    CustomerMasterComponent,
    ContractorMasterComponent,
    SupplierMasterComponent,
    BOMComponent,
    PurchaseOrderComponent,
    SalesOrderComponent,
    WoInhouseComponent,
    WoJobworkComponent,
    QualityControlComponent,
    SpinnerComponent,
    QualityAuditComponent,
    PlanningComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    FlexLayoutModule,
    ToastrModule.forRoot(),
  ],
  providers: [LoginService, UserAccountService, HeaderStorageService, AuthGuard, NavBarMenuService, UOMService, ProductService, BOMMasterService,
    CustomerService, SubContractService, RawMaterialService,SupplierService,CountryService,PurchaseOrderService,SalesOrderMasterService,WorkorderService,QualityAuditService,
    // AreaServicesService,CityServicesService,CountryServicesService,StateServicesService,
    { provide: MAT_DATE_LOCALE, useValue: "en-us" },
    { provide: DateAdapter, useClass: AppDateAdapter },
    { provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS }],
  bootstrap: [AppComponent]
})
export class AppModule { }
