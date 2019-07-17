import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatTableDataSource, MatSort, MatPaginator, MatDialog } from '@angular/material';
import { HttpClient } from '@angular/common/http';
import { BOM } from 'src/app/shared/models/bom';
import { ProductMaster, UOMMaster } from 'src/app/shared/models/product';
import { BOMMasterService } from 'src/app/shared/services/master-services/bom.service';
import { ProductService } from 'src/app/shared/services/master-services/product.service';
import { RawMaterialService } from 'src/app/shared/services/master-services/rawMaterial.service';
import { rmName } from 'src/app/shared/models/rawmaterial';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-bom',
  templateUrl: './bom.component.html',
  styleUrls: ['./bom.component.scss']
})
export class BOMComponent implements OnInit {
  displayeNewGrid = ['SNo', 'RMName', 'UOM','Quantity','Action'];
  @ViewChild('BomDialog') BomDialog: TemplateRef<any>;
  model: BOM = new BOM();
  public show: boolean = false;
  listarea: any = [];
  API_URL: string;
  bommaster: BOM[] = [];
  selection = new SelectionModel<Element>(true, []);
  public BomForm: FormGroup;
  productData: ProductMaster[] = [];
  Name: rmName[] = [];
  UOMData: UOMMaster[] = [];
  editFlag: string;
  model1:any={};
 BOMdetails: any = [];
  datasourceone = new MatTableDataSource<Element>(this.BOMdetails);
  bomdet: any;

  constructor(public form: FormBuilder, private _http: HttpClient, private _dialog: MatDialog,
    private _BOMMasterService: BOMMasterService, private _ProductService: ProductService,
    private _RawMaterialService: RawMaterialService, private _toastr: ToastrService,
  ) {
    this.buildForm();
  }

  dataSource = new MatTableDataSource();
  displayedColumns = ['SNo', 'BOMCode', 'BOMName', 'Product', 'UOM',  'action'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  saveBOMMaster() {
    debugger;
    const saveBom = new BOM();
    const arrBOMdetails = [];
    for (var start in this.BOMdetails) {
      if (this.BOMdetails[start]) {
        arrBOMdetails.push(this.BOMdetails[start]);
      }
    }
    saveBom.BOMCode = this.model.BOMCode;
    saveBom.BOMName = this.model.BOMName;
    saveBom.UOMID = this.model.UOMID;
    saveBom.prdID = this.model.prdID;
    saveBom.IsActive = 0;
    saveBom.BOMDetails = JSON.stringify(arrBOMdetails);
    if (this.editFlag === 'save') {
      debugger;

      this._BOMMasterService.insertBOMMaster(saveBom).subscribe(response => {
        if (response == true) {
          this.BomForm.reset();
          this._dialog.closeAll();
          this._toastr.success('BOM added Successfully!')
        }
      });
    }
    if (this.editFlag === 'update') {
      // saveBom.ChkListID = this.value.ChkListID;
      // saveBom.BOMdetails = JSON.stringify(this.BOMdetails);
      this._BOMMasterService.updateBOMMaster(this.model).subscribe(response => {
        if (response == true) {
          this.BomForm.reset();
          this._dialog.closeAll();
          this._toastr.success('BOM updated Successfully!')
        }
      })
    }


  }

  updateBOMMaster(element) {
    this.show = true;
    this.getBOMMaster(element.BOMID);
    this.editFlag = 'update';
    this.model = element;
  }


  getBOMMaster(BOMID) {
    debugger
    this._BOMMasterService.getBOMMaster(BOMID).subscribe(response => {
      debugger
      response['result'];
      if (response && BOMID >0) {
        this.bommaster = response.BOM_master;
        this.bomdet = response.BOM_details;
        this.dataSource = new MatTableDataSource(this.bommaster);
        this.datasourceone =  new MatTableDataSource(this.bomdet);
      }
      else{
        this.bommaster = response.BOM_master
        this.dataSource = new MatTableDataSource(this.bommaster);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }
    })
  }

  getProductMaster() {
    debugger;
    this._ProductService.getproductMaster().subscribe(response => {
      debugger
      response['result'];
      if (response) {
        this.productData = response;
        console.log("prdMaster", this.productData)
      }
    });
  }

  getRMName() {
    debugger
    this._RawMaterialService.getRawmaterial().subscribe(response => {
      debugger
      response['result'];
      if (response) {
        this.Name = response;
      }
    });
  }
  getUOMMaster() {
    debugger
    this._ProductService.getUOMMaster().subscribe(response => {
      debugger
      response['result'];
      if (response) {
        this.UOMData = response;
      }
    });
  }



  ngOnInit() {
    const obj= 0;
    this.getBOMMaster(obj);
    this.getProductMaster();
    this.getRMName();
    this.getUOMMaster();
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
    this.BomForm = this.form.group({
      BOMCode: ['', [Validators.required]],
      BOMName: ['', [Validators.required]],
      uom: ['', [Validators.required]],
      prdID: ['', [Validators.required]],
     // RMID: ['', [Validators.required]],
      //quantity: ['', [Validators.required]],

    });

  }
  addBom() {
    this.show = true;
    this.editFlag = 'save';

  }
  closeDialog() {
    this._dialog.closeAll();
    this.BomForm.reset();
  }
  backQCListForm(){
    this.show =false
  }

  addBOMField(model1) {
    debugger
    model1.ChkName = (model1.ChkName);
    this.setQcRow(model1);
  }
  checkFlag: boolean;
  Isediting: boolean = false;
  index: any;
  setQcRow(model1) {
    debugger;
    this.checkFlag = false;
    if (this.Isediting == true) {

      if (this.index != undefined) {
        this.BOMdetails[this.index] = model1;
      } else {
        this.BOMdetails.push(model1);
      }

    } else {
      this.BOMdetails.push(model1);
    }

    this.model1 = {};
    this.datasourceone = new MatTableDataSource(this.BOMdetails);
    console.log(this.datasourceone)
    this.Isediting = false;
  }


  deleteBOMdetails(index) {

    this.BOMdetails.splice(index, 1);
    this.datasourceone = new MatTableDataSource(this.BOMdetails);
  }
  updateBOMdetails(element, Isediting, index) {
    debugger;
    this.checkFlag = true;
    this.Isediting = Isediting;
    this.model1 = element;
    this.index = index;
    console.log(element, this.model);
  }

}
