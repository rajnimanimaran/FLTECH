import { Component, OnInit, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import { MatSelectChange, MatPaginator, MatSort } from '@angular/material';
export interface PeriodicElement {
  qty: number;
  itemno: number;
  itemcode: number;
  assignedQty: number;
  balanceQty: number;
  assignedto: string;

  price: number;
  tax: number;
  hsn: string;

}
export interface WOElement {
  SNo: number;
  itemcode: string;
  WorkorderSNo: string;
  assignedto: string;
  status: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  { itemno: 1, itemcode: 1, qty: 100, price: 100, tax: 5, hsn: 'p123', assignedQty: 0, balanceQty: 0, assignedto: 'x' },
  { itemno: 1, itemcode: 2, qty: 150, price: 50, tax: 10, hsn: 'p12', assignedQty: 0, balanceQty: 0, assignedto: 'x' },
  { itemno: 1, itemcode: 3, qty: 40, price: 170, tax: 5, hsn: 'a1', assignedQty: 0, balanceQty: 0, assignedto: 'x' },

];
const Wo_Data: WOElement[] = [
  { SNo: 1, itemcode: '1p', WorkorderSNo: '1Pipe', assignedto: 'abc', status: 'approved' },
  { SNo: 1, itemcode: '1m', WorkorderSNo: '1motor', assignedto: 'xyz', status: 'rejected' },
  { SNo: 1, itemcode: '1n', WorkorderSNo: '1nut', assignedto: 'qwe', status: 'rework' },
]
export interface Assigned {
  value: number;
  viewValue: string;
}
@Component({
  selector: 'app-wo-jobwork',
  templateUrl: './wo-jobwork.component.html',
  styleUrls: ['./wo-jobwork.component.scss']
})
export class WoJobworkComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  WOAssign = [];
  opengrid1: boolean;
  @Output()
  selectionChange: EventEmitter<MatSelectChange>
  opengrid2: boolean;
  jobmodel: any = {};
  @ViewChild('input1') inputEl: ElementRef;
  constructor() { }


  assignedTo: Assigned[] = [
    {value: 1, viewValue: 'sri'},
    {value: 2, viewValue: 'bala'},
    {value: 3, viewValue: 'sribala'}
  ]; 
  qtyOperation(ind) {
    debugger
    if (ELEMENT_DATA[ind].qty < ELEMENT_DATA[ind].assignedQty) {
      alert('Assigneed Quantity Should be less than Quantity')
      setTimeout(() => this.inputEl.nativeElement.focus());
    }
    else {
      ELEMENT_DATA[ind].balanceQty = ELEMENT_DATA[ind].qty - ELEMENT_DATA[ind].assignedQty
    }
  }


  showGridNo1() {
    this.opengrid1 = true;
    this.opengrid2 = false; //and should reset value in table 
  }
  showGridNo2() {
    // alert(this.dataSource.length)
    if(this.WOAssign.length >0){
    this.WOAssign = [];
    }
    debugger;
    let count = 0;
    for (let i = 0; i < this.dataSource.length; i++) {
      for (let j = 0; j < this.dataSource[i].assignedQty; j++) {
        this.WOAssign.push({
          'SNo': count + 1,
          'itemcode': this.dataSource[i].itemcode,
          'WorkorderSNo': count + 1,
          'assignedto': this.dataSource[i].assignedto

        })
        count = count + 1;
      }

      // for(let j = 0 ; j< this.dataSource[i].assignedQty)
    }



    this.opengrid2 = true;
  }
  displayedColumns: string[] = ['itemno', 'itemcode', 'qty', 'price', 'tax', 'hsn', 'assignedQty', 'balanceQty', 'assignedto'];
  displayedColumns1: string[] = ['SNo', 'itemcode', 'WorkorderSNo', 'assignedto', 'status'];
  dataSource = ELEMENT_DATA;
  dataSource1 = this.WOAssign;
  ngOnInit() {
    // this.dataSource.paginator = this.paginator;
    // this.dataSource.sort = this.sort;
  }

}
