import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { NavbarComponent } from './pages/navbar/navbar.component';
import { AuthGuard } from './shared/directives/authGuard';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UomMasterComponent } from './pages/master/uom-master/uom-master.component';
import { RawMaterialMasterComponent } from './pages/master/raw-material-master/raw-material-master.component';
import { ProductMasterComponent } from './pages/master/product-master/product-master.component';
import { CustomerMasterComponent } from './pages/master/customer-master/customer-master.component';
import { SupplierMasterComponent } from './pages/master/supplier-master/supplier-master.component';
import { ContractorMasterComponent } from './pages/master/contractor-master/contractor-master.component';
import { BOMComponent } from './pages/bom/bom.component';
import { PurchaseOrderComponent } from './pages/purchase-order/purchase-order.component';
import { SalesOrderComponent } from './pages/sales-order/sales-order.component';
import { WoInhouseComponent } from './pages/Workorder master/wo-inhouse/wo-inhouse.component';
import { WoJobworkComponent } from './pages/Workorder master/wo-jobwork/wo-jobwork.component';
import { QualityControlComponent } from './pages/master/quality-control/quality-control.component';
import { QualityAuditComponent } from './pages/quality-audit/quality-audit.component';
import { PlanningComponent } from './pages/planning/planning.component';

const routes: Routes = [{
  path: '',
  component: LoginComponent,
},
{
  path: 'login',
  component: LoginComponent,
},
{
  path: '',
  component: NavbarComponent,
  canActivate: [AuthGuard],
  children: [
    {
      path: 'dashboard',
      pathMatch: 'full',
      component: DashboardComponent,

    },
    {
      path: 'uom',
      component: UomMasterComponent,
    },
    {
      path: 'rawmaterial',
      component: RawMaterialMasterComponent,
    },
    {
      path: 'product',
      component: ProductMasterComponent,
    },
    {
      path: 'customer',
      component: CustomerMasterComponent,
    },
    {
      path: 'supplier',
      component: SupplierMasterComponent,
    },
    {
      path: 'contractor',
      component: ContractorMasterComponent,
    },
    {
      path     : 'qualitycontrol',
      component: QualityControlComponent,
    },
    {
      path: 'bom',
      component: BOMComponent,
    },
    {
      path: 'purchasedetails',
      component: PurchaseOrderComponent
    },
    {
      path: 'salesdetails',
      component: SalesOrderComponent
    },
    {
      path: 'wo-inhouse',
      component: WoInhouseComponent,
    },
    {
      path: 'wo-jobwork',
      component: WoJobworkComponent,
    },
    {
      path:'qualityaudit',
      component:QualityAuditComponent,
    },{
      path:'planning',
      component:PlanningComponent,
    }
  ]
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
