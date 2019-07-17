import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog, MatPaginator, MatSort } from '@angular/material';
import { SupplierModel } from 'src/app/shared/models/supplier';
import { SupplierService } from 'src/app/shared/services/master-services/supplier.service';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
@Component({
  selector: 'app-supplier-master',
  templateUrl: './supplier-master.component.html',
  styleUrls: ['./supplier-master.component.scss']
})
export class SupplierMasterComponent implements OnInit {
  displayedColumns = ['SNo', 'SupplierName', 'SupplierPhNo', 'GSTNo', 'State', 'StateCode', 'action'];
  @ViewChild('supplierDialog') supplierDialog: TemplateRef<any>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  model: SupplierModel = new SupplierModel();
  Supplier: SupplierModel[] = [];
  isEditing = false;
  editFlag: string = '';
  public SupplierForm: FormGroup;
  dataSource: MatTableDataSource<SupplierModel>;
  isLoading: boolean;
  constructor(public _dialog: MatDialog, private _supplierservice: SupplierService, private _toastr: ToastrService, 
    public form: FormBuilder) {
      this.buildForm();
  }
  public buildForm() {
    this.SupplierForm = this.form.group({
      SName: ['', [Validators.required]],
      SPhNo: ['', [Validators.required]],
      SGSTNo: ['', [Validators.required]],
      SState: ['', [Validators.required]],
      SStateCode: ['', [Validators.required]],
      SPan: ['', [Validators.required]],
      SAddress: ['', [Validators.required]],
    });
  }

  ngOnInit() {
    this.getQualityAudit();
  }

  getQualityAudit() {
    this._supplierservice.getQuality().subscribe(response => {
      if (response) {
        this.isLoading = true;
        this.Supplier = response;
        this.dataSource = new MatTableDataSource(this.Supplier);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }

    })

  }

  saveQuality() {
    debugger
    if (this.editFlag === 'save') {
      this._supplierservice.insertQuality(this.model).subscribe(response => {
        if (response == true) {
          this.SupplierForm.reset();
          this._dialog.closeAll();
          this._toastr.success('Supplier added Successfully!')
        }
      });
    }
    else if(this.editFlag === 'update') {
      this._supplierservice.updateQuality(this.model).subscribe(response => {
        if (response) {
          this.SupplierForm.reset();
          this._dialog.closeAll();
          this._toastr.success('Supplier Updated Successfully!')
        }
      })
    }
  }

  updateProducts(element) {
    this.addSupplier();
    this.editFlag = 'update';
    this.model = element;

  }

  addSupplier() {
    this.editFlag = 'save';
    const dialogRef = this._dialog.open(this.supplierDialog, {
      panelClass: 'Dialog',
      hasBackdrop: false
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getQualityAudit();
    })
  }
  
  closeDialog() {
    this._dialog.closeAll();
  }

}