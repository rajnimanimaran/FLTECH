import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { MatDialog, MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { QualityAuditService } from 'src/app/shared/services/quality-audit.service';

@Component({
  selector: 'app-quality-audit',
  templateUrl: './quality-audit.component.html',
  styleUrls: ['./quality-audit.component.scss']
})
export class QualityAuditComponent implements OnInit {
  displayedColumns: string[] = ['ItemNo', 'ItemCode', 'Qty', 'ApprovalQty', 'RejectedQty', 'ReworkQty'];
  dataSource = new MatTableDataSource();
  displayedColumns1: string[] = ['SNo', 'AssignedTo', 'Status', 'Audit'];
  dataSource1 = new MatTableDataSource();
  displayedColumns2: string[] = ['CheckId', 'CheckDescription', 'Result'];
  dataSource2 = new MatTableDataSource();
  tableShow = false;
  itemdetailView = false;
  WOSdetails: any = [];
  ResultStatus: any
  FinalResult:any;
  @ViewChild('qualityAuditDialog') qualityAuditDialog: TemplateRef<any>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(public _dialog: MatDialog, private _qualityAudit: QualityAuditService) { }
  model: any = []
  workorderData: any = []
  workorderdetails: any = []
  ngOnInit() {
    const Id = 0;
    this.getWorkorder(Id);
  }
  getWorkorder(obj) {
    debugger;
    this._qualityAudit.getWorkorder(obj).subscribe(
      (res) => {
        this.workorderData = res;
        console.log(this.workorderData)
      }
    )
  }
  AddproductCategorydetails() {

  }



  selectoption(id) {
    this.tableShow = true;
    this._qualityAudit.getWorkorder(id).subscribe((res) => {
      this.workorderdetails = res;
      this.dataSource = new MatTableDataSource(this.workorderdetails);
    })
  }

  getWorkorderdetail(workorderID) {
    this.itemdetailView = true;
    this._qualityAudit.getWorkorderdetail(workorderID).subscribe((res) => {
      this.WOSdetails = res;
      this.dataSource1 = new MatTableDataSource(this.WOSdetails);
    })
  }

  QCdetails: any = [];
  addContractor(prdID) {
    this._qualityAudit.getQCdetail(prdID).subscribe((res) => {
      this.QCdetails = res;
      this.dataSource2 = new MatTableDataSource(this.QCdetails);
    })

    const dialogRef = this._dialog.open(this.qualityAuditDialog, {
      panelClass: 'Dialog',
      width: '50%',
      hasBackdrop: false
    });
    dialogRef.afterClosed().subscribe(result => {
    })
  }
  closeDialog() {
    this._dialog.closeAll();
  }

}





