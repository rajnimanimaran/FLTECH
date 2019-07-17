import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator, MatDialog, MatDialogConfig } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SelectionModel } from '@angular/cdk/collections';
import { environment } from '../../../../environments/environment';
// import { UOMService } from '../../../Services/Master/uom.service';
import { HttpClient } from '@angular/common/http';
import { Uom } from '../../../shared/models/uom';
import { UOMService } from 'src/app/shared/services/master-services/uom.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-uom-master',
  templateUrl: './uom-master.component.html',
  styleUrls: ['./uom-master.component.scss'],
  providers: [MatDialogConfig]
})
export class UomMasterComponent implements OnInit {

  public show: boolean = false;
  isLoading = false;
  editFlag: string = '';
  selection = new SelectionModel<Element>(true, []);
  public ProductForm: FormGroup;
  API_URL: string;
  model: any = {};
  uom: Uom[] = [];
  screenfullClicked = false;

  constructor(public form: FormBuilder, public _dialog: MatDialog, private _http: HttpClient, private _toastr: ToastrService,
    private _uomService: UOMService
  ) {
    this.API_URL = environment.apiUrl;
  }

  dataSource = new MatTableDataSource();
  displayedColumns = ['SNo', 'UOMCode', 'UOMName', 'Multifier', 'BaseUnit', 'action'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('uomDialog') uomDialog: TemplateRef<any>;

  saveUOM() {
    if (this.editFlag === 'save') {
      this._uomService.insert(this.model).subscribe(response => {
        if (response == true) {
          this.model = {};
          this._dialog.closeAll();
          this._toastr.success('UOM added Successfully!')
        }
      });
    }
    if (this.editFlag === 'update') {
      this._uomService.update(this.model).subscribe(response => {
        if (response == true) {
          this.model = {};
          this._dialog.closeAll();
          this._toastr.success('UOM updated Successfully!')
        }
      })
    }
  }
  updateUOM(ele) {
    this.addUom();
    this.model = ele;
    this.editFlag = 'update';

  }
  getUOM() {
    this._uomService.get().subscribe(response => {
      response['result'];
   
      if (response) {
        this.isLoading = true;
        this.uom = response
        this.dataSource = new MatTableDataSource(this.uom);
        this.dataSource.paginator = this.paginator;
      }
      error => this.isLoading = true
    })
  }
  ngOnInit() {
    this.getUOM();
  }
  addUom() {
    this.editFlag = 'save';
    const dialogRef = this._dialog.open(this.uomDialog, {
      panelClass: 'Dialog',
      hasBackdrop: false
    })
    dialogRef.afterClosed().subscribe(result => {
      this.getUOM();
    })
  }

  closeDialog() {
    this._dialog.closeAll();
    this.model = {}
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();         // Remove whitespace
    filterValue = filterValue.toLowerCase();  // MatTableDataSource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }

  handleClick(event) {
    event.stopPropagation();
  }

  public buildForm() {
    this.ProductForm = this.form.group({
      UOMCode: ['', [Validators.required]],
      UOMName: ['', [Validators.required]],
      Multifier: ['', [Validators.required]],
      BaseUnit: ['', [Validators.required]],
    });
  }
}
