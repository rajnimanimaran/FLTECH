import { Component, OnInit, ViewChild, TemplateRef, ElementRef } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { FormBuilder, FormControl } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { SalesOrderMasterService } from 'src/app/shared/services/sales-order.service';
import { FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

export class SalesOrderMaster {
  OrderID: Number;
  detlId: String;
  OrderSetID: Number;
  ordAckSdate: String;
}


export interface planning {
  quantity: Number;
  deliverydate: String;
}

export interface plandetail {
  fields: string;
}


@Component({
  selector: 'app-supply-against-planning',
  templateUrl: './supply-against-planning.component.html',
  styleUrls: ['./supply-against-planning.component.scss'],
  providers: [DatePipe]
})
export class SupplyAgainstPlanningComponent implements OnInit {

  today = new Date();
  displayedgridcolumns: string[] = ['S.No', 'ProductName', 'quantity', 'UOM', 'action'];
  PlanningForm: FormGroup;
  displayedColumns = ['quantity', 'deliverydate'];
  codeList = [];
  model1: SalesOrderMaster = new SalesOrderMaster();
  Btntitle: string = 'Save'
  // ELEMENT_DATA2: plandetail[] = [
  //   {fields: 'Material Recieve in Factory'},
  //   {fields: 'Material in Flow'},
  //   {fields: 'Material Recieve in Factory'},
  //   {fields: 'Assemble'},
  //   {fields: 'Hydraulic'},
  //   {fields: 'Perfect Test'},
  //   {fields: 'Painting'},
  //   {fields: 'Final Inspection'},
  //   {fields: 'Packing'}
  // ];

  // displayedColumns1 = ['fields','startdate', 'enddate'];
  // dataSource2 = this.ELEMENT_DATA2;


  @ViewChild('PlanDialog', null) PlanDialog: TemplateRef<any>;
  @ViewChild('PlanViewDialog', null) PlanViewDialog: TemplateRef<any>;
  categoryModel: any = [];
  show = false;
  model: any = {}
  plan: any = {};
  isEditing = false;
  getSalesData: any = [];
  getSalesPlanned = [];
  salesOrderData = new MatTableDataSource<any>(this.getSalesData);
  dataSource1 = new MatTableDataSource<any>(this.getSalesPlanned);
  // dataSource = new MatTableDataSource<any>(this.OrderFormDetail);
  // displayedgridcolumns = ['SNo', 'OrderNumber', 'SName', 'OrderDate', 'ACKDate', 'action']
  // displayedColumns = ['SNo', 'ProductName', 'quantity', 'UOM', 'GSTNo', 'Rate', 'Tax', 'Amount', 'TAmount', 'action'];
  isLoading: boolean = true;
  OrderFormDetail: any = [];
  totalQuantity: any;
  planningView: any;
  disableAddPlan = false;
  grantQty: any;
  constructor(
    private _salesorderservice: SalesOrderMasterService,
    private toastr: ToastrService, private _dialog: MatDialog,
    private form: FormBuilder
  ) {

  }


  ngOnInit() {
    this.getSOCode();
  }

  getSOCode() {
    this._salesorderservice.getSalesOrderCode().subscribe(res => {
      if (res) {
        this.codeList = res;
        console.log(this.codeList, 'codelist')
      }
    });
  }

  formatDate(date) {
    var monthNames = [
      "January", "February", "March", "April", "May", "June", "July",
      "August", "September", "October", "November", "December"];

    var day = date.getDate();
    var monthIndex = date.getMonth();
    var year = date.getFullYear();

    return day + ' ' + monthNames[monthIndex] + ' ' + year;

  }
  Sodetail: any
  codeControl = new FormControl();
  getSoDetails(event) {
    this.Sodetail = event.value
    // this.model1 = event.value;
    this.model1.OrderID = event.value.OrderID;
    this._salesorderservice.getSalesOrder(this.model1.OrderID).subscribe(response => {
      if (response) {
        debugger;
        this.model = response.SalesOrderMaster[0];
        this.OrderFormDetail = response.SalesOrderDetails;
        // this.salesOrderData = new MatTableDataSource(this.OrderFormDetail);
        console.log(this.OrderFormDetail, 'salesOrderData')
      }
    });
  }


  getGrandquantity() {
    this.grantQty = this.getSalesPlanned.map(t => t.Quantity).reduce((acc, value) => acc + value, 0);
  }
  @ViewChild('quantity') inputEl: ElementRef;
  checkQty: any;
  checkSupplyQty(data, qty) {
    debugger
    this.checkQty = qty
    if (qty > (data.Quantity - data.SuppliedQuantity)) {
      alert("Entered Quantity should be lesser than " + "" + (data.Quantity - data.SuppliedQuantity))
      setTimeout(() => this.inputEl.nativeElement.focus());
    }
  }
  SuppliedQty: any;
  supplyPlanned = [];
  saveSupply(supply) {
    this.supplyPlanned = [];
    if (this.checkQty > (supply.Quantity - supply.SuppliedQuantity)) {
      alert("Entered Quantity should be lesser than " + "" + (supply.quantity - supply.SuppliedQuantity))
    } else {
      this.supplyPlanned.push(supply)
      this.supplyPlanned.map(x => x.SuppliedQty = parseInt(supply.SuppliedQty))
      this.supplyPlanned.map(x => x.SuppliedQuantity = parseInt(supply.SuppliedQty));
      this._salesorderservice.savesupply(this.supplyPlanned[0]).subscribe((res) => {
        if (res) {
          this._salesorderservice.getSalesOrderPlan(supply.SORefID).subscribe(response => {
            if (response) {
              debugger;
              this.model = response;
              this.getSalesPlanned = response;
              this.dataSource1 = new MatTableDataSource(this.getSalesPlanned);
            }
          });
          this.toastr.success("Added Successfully");
        }
      })
    }

  }

  showPlan(event) {
    debugger;
    this.totalQuantity = event.value.Quantity;
    this.model1.detlId = event.value.DetlID;
    this._salesorderservice.getSalesOrderPlan(this.model1.detlId).subscribe(response => {
      if (response) {
        debugger;
        this.model = response;
        this.getSalesPlanned = response;
        this.dataSource1 = new MatTableDataSource(this.getSalesPlanned);
        console.log(this.getSalesPlanned, "getSalesPlanned")
      }
      this.getGrandquantity();
      if (this.grantQty === this.totalQuantity) {
        this.disableAddPlan = true;
      }
    });
  }

  @ViewChild(MatPaginator, null) paginator: MatPaginator;
  @ViewChild(MatSort, null) sort: MatSort;



  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
  }

  handleClick(event) {
    event.stopPropagation();
  }

  screenfullClicked = false;
  onClick() {
    this.screenfullClicked = !this.screenfullClicked;
  }

  openPlanDetails(data) {
    debugger;
    this.planningView = data;
    // this.getSoDetails(data.OrderID);
    this._dialog.open(this.PlanViewDialog, {
      panelClass: 'Dialog',
      width: '800px',
    })
  }

  eventkeypress() {
    this.keyPressQty(event);
  }
  keyPressQty(event: any) {
    const pattern = /[0-9\+\-\ ]/;
    let inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }

  onlyNumber(event: any) {
    const pattern = /[0-9\+\-\ ]/;
    let inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }
  onlyCharacter(event: any) {
    const pattern = /[a-zA-Z\+\-\ ]/;
    let inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }
}
