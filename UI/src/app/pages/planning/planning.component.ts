import { Component, OnInit, ViewChild, TemplateRef, ElementRef } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { FormBuilder, FormControl } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { SalesOrderMasterService } from 'src/app/shared/services/sales-order.service';
import { FormGroup, Validators } from '@angular/forms';

export class SalesOrderMaster {
  OrderID: Number;
  detlId: String;
  OrderSetID: Number;
}
export interface planning {
  quantity: Number;
  deliverydate: String;
}

export interface plandetail {
  fields: string;
}
@Component({
  selector: 'app-planning',
  templateUrl: './planning.component.html',
  styleUrls: ['./planning.component.scss'],
  providers: [DatePipe]
})
export class PlanningComponent implements OnInit {


  displayedgridcolumns: string[] = ['S.No', 'ProductName', 'quantity', 'UOM', 'action'];
  PlanningForm: FormGroup;
  displayedColumns = ['quantity', 'deliverydate','action'];
  codeList = [];
  model1: SalesOrderMaster = new SalesOrderMaster();
  Btntitle : string = 'Save'
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
  dataSource1 = new MatTableDataSource<any>(this.getSalesData);
  // dataSource = new MatTableDataSource<any>(this.OrderFormDetail);
  // displayedgridcolumns = ['SNo', 'OrderNumber', 'SName', 'OrderDate', 'ACKDate', 'action']
  // displayedColumns = ['SNo', 'ProductName', 'quantity', 'UOM', 'GSTNo', 'Rate', 'Tax', 'Amount', 'TAmount', 'action'];
  isLoading: boolean = true;
  OrderFormDetail = [];
  totalQuantity: any;
  planningView:any;
  disableAddPlan=false;
  grantQty: any;
  constructor(
    private _salesorderservice: SalesOrderMasterService,
    private _datePipe: DatePipe, private _dialog: MatDialog,
    private form: FormBuilder
  ) {

  }
  initFormGroup() {
    this.PlanningForm = this.form.group({
      quantity: ['', [Validators.required]],
      sORefID: ['', Validators.required],
      sOID: ['', Validators.required],
      OrderSetID: [0],
      deliveryDate: ['', Validators.required],
      ordAckSdate: ['', [Validators.required]],
      ordAckEdate: ['', [Validators.required]],
      matRcvInFacSdate: ['', [Validators.required]],
      matRcvInFacEdate: ['', [Validators.required]],
      matInFlowSdate: ['', [Validators.required]],
      matInFlowEdate: ['', [Validators.required]],
      assembleSdate: ['', [Validators.required]],
      assembleEdate: ['', [Validators.required]],
      hydrolicSdate: ['', [Validators.required]],
      hydrolicEdate: ['', [Validators.required]],
      perfTestSdate: ['', [Validators.required]],
      perfTestEdate: ['', [Validators.required]],
      paintingSdate: ['', [Validators.required]],
      paintingEdate: ['', [Validators.required]],
      finalInspSdate: ['', [Validators.required]],
      finalInspEdate: ['', [Validators.required]],
      packingSdate: ['', [Validators.required]],
      packingEdate: ['', [Validators.required]],
    })
  }


  ngOnInit() {
    this.initFormGroup();
    this.getSOCode();
  }

  getSOCode() {
    this._salesorderservice.getSalesOrderCode().subscribe(res => {
      if (res) {
        this.codeList = res;
      }
    });
  }
  savePlanningDetails() {
    this.PlanningForm.controls.sOID.setValue(this.model1.OrderID);
    this.PlanningForm.controls.sORefID.setValue(this.model1.detlId);
    if (this.PlanningForm.valid) {
      this._salesorderservice.saveSOPlanning(this.PlanningForm.value).subscribe(resp => {
        if (resp) {
          this._dialog.closeAll();
          this.addPlan(this.model1.detlId,this.totalQuantity);
          this.isEditing=false;
        }
      })
    }
  }
  codeControl = new FormControl();
  getSoDetails(event) {
 this.model1.OrderID=event.value.OrderID;
    this._salesorderservice.getSalesOrder(this.model1.OrderID).subscribe(response => {
      if (response) {
        debugger;
        this.model = response.SalesOrderMaster[0];
        this.OrderFormDetail = response.SalesOrderDetails;
        this.salesOrderData = new MatTableDataSource(this.OrderFormDetail);
        console.log(this.OrderFormDetail)
      }
    });
  }
  getGrandquantity() {
    this.grantQty= this.getSalesPlanned.map(t => t.Quantity).reduce((acc, value) => acc + value, 0);
  }
  @ViewChild('quantity') inputEl: ElementRef;
  checkQuantity(qty) {
    debugger;
   
    if (qty > this.totalQuantity - this.grantQty) {
      alert("Entered Quantity should be lesser than " + "" + (this.totalQuantity - this.grantQty))
      setTimeout(() => this.inputEl.nativeElement.focus());
    }
  }
  addPlan(detlId, Quantity) {
    this.disableAddPlan = false;
    this.totalQuantity = Quantity;
    this.model1.detlId = detlId;    
    this.show = true;
    this._salesorderservice.getSalesOrderPlan(detlId).subscribe(response => {
      if (response) {
        debugger;
        this.model = response;
        this.getSalesPlanned = response;
        this.dataSource1 = new MatTableDataSource(this.getSalesPlanned);
        console.log(this.getSalesPlanned)
      }
      this.getGrandquantity();
       if(this.grantQty===this.totalQuantity){
         this.disableAddPlan =true;
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
    this.planningView=data;
    // this.getSoDetails(data.OrderID);
    this._dialog.open(this.PlanViewDialog, {
      panelClass: 'Dialog',
      width: '800px',
    })
  }

  updatePlanDetailsgrid(data){
    debugger;
    this.Btntitle  = 'Update';
    this.isEditing = true;
    this.PlanningForm.controls.OrderSetID.setValue(data.OrderSetID);
    // this.planningView=data;
    this.model = data;
    this._dialog.open(this.PlanDialog, {
      panelClass: 'Dialog',
      width: '800px',
      height: '700px',
      disableClose: true
    })
  }
  closeDialog() {
    this.isEditing = false;
    this._dialog.closeAll();
  }

  openPlanDetail() {
    this.PlanningForm.reset();
    this.model={};
    this.Btntitle  = 'Save';
    this._dialog.open(this.PlanDialog, {
      panelClass: 'Dialog',
      width: '800px',
      height: '700px',
      disableClose: true
    })
  }
  closeaddplan() {
    this.show = false;
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
