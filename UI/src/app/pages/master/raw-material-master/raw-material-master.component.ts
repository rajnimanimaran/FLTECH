import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { RawMaterial } from 'src/app/shared/models/rawmaterial';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { HttpClient } from '@angular/common/http';
import { RawMaterialService } from 'src/app/shared/services/master-services/rawMaterial.service';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/shared/services/master-services/product.service';
import { UOMMaster } from 'src/app/shared/models/product';

@Component({
  selector: 'app-raw-material-master',
  templateUrl: './raw-material-master.component.html',
  styleUrls: ['./raw-material-master.component.scss']
})
export class RawMaterialMasterComponent implements OnInit {
  editFlag: string = '';
  model: RawMaterial = new RawMaterial();
  public show: boolean = false;
  isEditing: boolean;
  listarea: any = [];
  API_URL: string;
  rawmaterial: RawMaterial[] = [];
  selection = new SelectionModel<Element>(true, []);
  public RawmaterialForm: FormGroup;
  @ViewChild('rawMaterialDialog') rawMaterialDialog: TemplateRef<any>;
  UOMData: UOMMaster[] = [];
  isLoading: boolean;
  constructor(public form: FormBuilder, private _http: HttpClient, public _dialog: MatDialog,
    private _RawmaterialService: RawMaterialService, private _toastr: ToastrService,
    private _ProductService: ProductService
  ) {
this.buildForm();
  }

  dataSource = new MatTableDataSource();
  displayedColumns = ['SNo', 'RawMaterialCode', 'RawMaterialName', 'UOM', 'Stock', 'ReOrderlevel', 'action'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  saveRawMaterial() {
    if (this.editFlag === 'save') {
      this._RawmaterialService.insertRawmaterial(this.model).subscribe(response => {
        if (response == true) {
          this.RawmaterialForm.reset();
          this._dialog.closeAll();
          this._toastr.success('Raw material added Successfully!')
        }
      });
    }
    else if (this.editFlag === 'update') {
      this._RawmaterialService.updateRawmaterial(this.model).subscribe(response => {
        if (response  == true) {
          this.RawmaterialForm.reset();
          this._toastr.success("Raw material updated sucessfully");
          this._dialog.closeAll();
        }
      })
    }
  }

  updateRawmaterial(element) {
    this.addrawMaterial();
    this.editFlag = 'update';
    this.model = element;
  }

 
  getRawmaterial() {
    this._RawmaterialService.getRawmaterial().subscribe(response => {
      if (response) {
        this.isLoading = true;
        this.rawmaterial = response;
        this.dataSource = new MatTableDataSource(this.rawmaterial);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }
    })
  }


  ngOnInit() {
    this.getRawmaterial();
    this.getUOMMaster();
  }

  getUOMMaster() {
    this._ProductService.getUOMMaster().subscribe(response => {
      if (response) {
        this.UOMData = response;
      }
    });
  }


  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }


  handleClick(event) {
    event.stopPropagation();
  }

  public buildForm() {
    this.RawmaterialForm = this.form.group({
      RMCode: ['', [Validators.required]],
      RMName: ['', [Validators.required]],
      uom: ['', [Validators.required]],
      stock: ['', [Validators.required]],
      re_Orderlevel: ['', [Validators.required]],
      sell_price: ['', [Validators.required]],
      cost_price: ['', [Validators.required]],
    });
  }
  addrawMaterial() {
    this.editFlag = 'save';
    const dialogRef = this._dialog.open(this.rawMaterialDialog, {
      panelClass: 'Dialog',
      hasBackdrop: false
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getRawmaterial();
    })
  }
  closeDialog() {
    this._dialog.closeAll();
    this.RawmaterialForm.reset();
  }
}
