import { Component, OnInit, Output, EventEmitter, ViewChild, ElementRef, TemplateRef } from '@angular/core';
import { MatSelectChange, MatPaginator, MatSort, MatTableDataSource, MatDialog } from '@angular/material';
import { SalesOrderMasterService } from 'src/app/shared/services/sales-order.service';
import { SubContractService } from 'src/app/shared/services/master-services/contractor.service';
import { WorkOrder } from 'src/app/shared/models/WorkOrder';
import { DatePipe } from '@angular/common';
import { WorkorderService } from 'src/app/shared/services/workorder.service';
import { SalesOrderMaster } from 'src/app/shared/models/sales-order';
export interface PeriodicElement {
  qty: number;
  itemno: number;
  itemcode: number;
  assignedQty: number;
  balanceQty: number;
  // assignedto: string;
}
export interface WOElement {
  SNo: number;
  itemcode: string;
  WorkorderSNo: string;
  // assignedto: string;
  status: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  { itemno: 1, itemcode: 1, qty: 100, assignedQty: 0, balanceQty: 0 },
  { itemno: 1, itemcode: 2, qty: 150, assignedQty: 0, balanceQty: 0 },
  { itemno: 1, itemcode: 3, qty: 40, assignedQty: 0, balanceQty: 0 },

];
// const Wo_Data: WOElement[] = [
//   { SNo: 1, itemcode: '1p', WorkorderSNo: '1Pipe', assignedto: 'abc', status: 'approved' },
//   { SNo: 1, itemcode: '1m', WorkorderSNo: '1motor', assignedto: 'xyz', status: 'rejected' },
//   { SNo: 1, itemcode: '1n', WorkorderSNo: '1nut', assignedto: 'qwe', status: 'rework' },
// ]
export interface Assigned {
  value: number;
  viewValue: string;
}
@Component({
  selector: 'app-wo-inhouse',
  templateUrl: './wo-inhouse.component.html',
  styleUrls: ['./wo-inhouse.component.scss'],
  providers: [DatePipe]
})
export class WoInhouseComponent implements OnInit {
    @ViewChild('confirmation') confirmation: TemplateRef<any>;
  getSerialDetails: any = [];
  WOAssign = new MatTableDataSource<any>(this.getSerialDetails);
  opengrid1: boolean;
  model: any = {};
  @Output()
  selectionChange: EventEmitter<MatSelectChange>
  opengrid2: boolean;
  jobmodel: any = {};
  @ViewChild('input1') inputEl: ElementRef;
  getSalesID: any = [];
  getSalesData: any = [];
  getSalesMasterData:any = [];
  salesOrder_WOData = new MatTableDataSource<any>(this.getSalesData);
  // dataSource1 = this.WOAssign;
  showError = false;
  isLoading: boolean= true;
  showGenerate: boolean;
 
  constructor(private _salesorderservice: SalesOrderMasterService, private _woService:WorkorderService,
    private _datePipe: DatePipe, private _subContractService: SubContractService, private _dialog: MatDialog) { }

  qtyOperation(ind) {
    debugger
    if (this.getSalesData[ind].BalanceQty < this.getSalesData[ind].AssignedQty) {
      this.showError = true;
      setTimeout(() => this.inputEl.nativeElement.focus());
      setTimeout(resp => {
        this.showError = false;
      }, 4000)
    }
    else {
      this.getSalesData[ind].BalanceQty = this.getSalesData[ind].BalanceQty - this.getSalesData[ind].AssignedQty

    }
  }


  showGridNo1(id) {
    this.showGenerate = false;
    this.getSalesOrderDetails(id);
    this.opengrid1 = true;
    this.opengrid2 = false; //and should reset value in table 
  }
  showGridNo2() {
    debugger
    // alert(this.dataSource.length)
    // if (this.WOAssign.length > 0) {
    //   this.WOAssign = [];
    // }
    // debugger;
    // let count = 0;
    // for (let i = 0; i < this.salesOrder_WOData.data.length; i++) {
    //   for (let j = 0; j < this.getSalesData[i].Quantity; j++) {
    //     debugger
    //     this.WOAssign.push({
    //       'SNo': count + 1,
    //       'itemcode': this.getSalesData[i].CODE,
    //       'WorkorderSNo': count + 1,
    //     })
    //     count = count + 1;
    //   }
    // }
    this.opengrid2 = true;
  }
  displayedColumns: string[] = ['itemno', 'itemcode','originalQty', 'qty', 'assignedQty', 'balanceQty'];
  displayedColumns1: string[] = ['SNo', 'itemcode', 'WorkorderSNo', 'status'];
  dataSource = new  MatTableDataSource();
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  ngOnInit() {
    this.getContract();
    const obj =0 ;
    this.getSalesOrderDetails(obj);
  }
  contract = [];
  getContract() {
    this._subContractService.getsubContract().subscribe(response => {
      if (response) {
        this.contract = response
        console.log(this.contract)
      }
    });
  }
  getSalesOrderDetails(obj) {
    debugger;
    this._salesorderservice.getSalesOrder(obj).subscribe(response => {
      if (response && obj > 0) {
        debugger;
        this.getSalesMasterData = response.SalesOrderMaster
        this.getSalesData = response.SalesOrderDetails;
        // for(let i = 0; i<this.getSalesData.length; i++){
        //   this.getSalesData[i].
        // }
        this.salesOrder_WOData = new MatTableDataSource(this.getSalesData);
      }
      else {
        this.getSalesID = response.SalesOrderMaster;
      }
    });
  }
  generateConfrimation() {
    this.showGenerate = true
    const dialogRef = this._dialog.open(this.confirmation, {
      panelClass: 'Dialog',
      hasBackdrop: false
    });
    dialogRef.afterClosed().subscribe(result => {
    })
  }

  closeDialog() {
    this._dialog.closeAll();
  }
  generateSerialTable() {
    this.closeDialog();
    this.isLoading = false  
    const WorkOrderForm = new WorkOrder();
    const orderForm = new SalesOrderMaster();
    //  orderForm.OrderID = this.model.OrderID;
     orderForm.OrderID = this.getSalesMasterData[0].OrderID;
      orderForm.CODE = this.getSalesMasterData[0].CODE;
      orderForm.CustId = this.getSalesMasterData[0].CustId;
      orderForm.OrderDate = new Date(this._datePipe.transform(this.getSalesMasterData[0].OrderDate, 'yyyy-MM-dd'));
      orderForm.ACKDate = new Date(this._datePipe.transform(this.getSalesMasterData[0].ACKDate, 'yyyy-MM-dd'));
      orderForm.AssignedTo = this.getSalesMasterData[0].AssignedTo;
      orderForm.OrderTakenBy = this.getSalesMasterData[0].OrderTakenBy;
      orderForm.OrderStatus =this.getSalesMasterData[0].OrderStatus;
     for(let i =0 ; i<this.getSalesData.length ; i++){
      this.getSalesData[i].AssignedQty = +this.getSalesData[i].AssignedQty;
    }
      orderForm.OrderDetails = JSON.stringify(this.getSalesData);
      this._salesorderservice.updateSalesOrder(orderForm).subscribe(response => {
        if (response) {
          this.model = {};
        } 
      });

    WorkOrderForm.OrderID = this.model.OrderID;
    WorkOrderForm.ContID = this.model.supplierID;
    WorkOrderForm.AssignedDate = new Date(this._datePipe.transform(this.model.assignedDate, 'yyyy-MM-dd'));
    WorkOrderForm.ExpectedDeliveryDate = new Date(this._datePipe.transform(this.model.ExpectedDeliveryDate, 'yyyy-MM-dd'));
    WorkOrderForm.isActive = 1;
    WorkOrderForm.status = 1;
    for(let i =0 ; i<this.getSalesData.length ; i++){
      this.getSalesData[i].Quantity = this.getSalesData[i].AssignedQty;
    }
    WorkOrderForm.OrderDetails = JSON.stringify(this.getSalesData);
    this._woService.insertWorkOrder(WorkOrderForm).subscribe(response => {
      if (response) {
        this.opengrid2 = true
        this.getSerialDetails = response.getWOSODetails;
        this.WOAssign = new MatTableDataSource(this.getSerialDetails);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isLoading = true;      
        this.model = {};
      }
    });

  }
}