import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { SelectionModel, DataSource } from '@angular/cdk/collections';
import { ProductMaster } from 'src/app/shared/models/product';
import { DropPName, QC_tMaster } from 'src/app/shared/models/quality-control';
import { QualitycontrolService } from 'src/app/shared/services/quality-control.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/shared/services/master-services/product.service';
// import { ProductService } from 'FT-UI/src/app/shared/services/master-services/product.service';
@Component({
  selector: 'app-quality-control',
  templateUrl: './quality-control.component.html',
  styleUrls: ['./quality-control.component.scss']
})
export class QualityControlComponent implements OnInit {

  public show: boolean = false;
  isEditing: boolean = false;
  Isediting: boolean = false
  @ViewChild('qCDialog') qCDialog: TemplateRef<any>;
  public QCForm: FormGroup;
  selection = new SelectionModel<Element>(true, []);
  productData: ProductMaster[] = [];
  editFlag = false;
  displayedColumns = ['SNo', 'QCCode', 'QCName', 'prdName', 'Action'];
  dataSource = new MatTableDataSource();

  displayeNewGrid = ['SNo', 'ChkName', 'Action'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort
  ddvalues: DropPName[] = [];
  QualityCheckList: any = [];
  DropPName = new DropPName();
  dropPName: DropPName[] = [];
  PrdData: DropPName[] = [];
  index: any;
  QCdetails: any = [];
  datasourceone = new MatTableDataSource<Element>(this.QCdetails);
  model1: any = {
    ChkName: ''
  };
  value: any = {};
  model: any = {};
  isLoading: boolean;
  product = [];
  checkFlag: boolean;


  constructor(private _QualitycontrolService: QualitycontrolService, private _ProductService: ProductService,
    private _dialog: MatDialog, public form: FormBuilder, private _toastr: ToastrService) {
    this.buildForm();
  }
  public buildForm() {
    this.QCForm = this.form.group({
      Code: ['', [Validators.required]],
      Name: ['', [Validators.required]],
      ProductName: ['', [Validators.required]],
    });
  }
  saveQuality() {
    debugger;
    const saveqc = new QC_tMaster();
    const arrQCDetails = [];
    for (var start in this.QCdetails) {
      if (this.QCdetails[start]) {
        arrQCDetails.push(this.QCdetails[start]);
      }
    }
    saveqc.QCID = this.model.QCID;
    saveqc.QCCode = this.model.QCCode;
    saveqc.QCName = this.model.QCName;
    saveqc.PrdID = this.model.PrdID;
    // saveqc.Description = this.model1.Description;

    if (this.value && this.isEditing == true) {
      saveqc.ChkListID = this.value.ChkListID;
      saveqc.QCDetails = JSON.stringify(this.QCdetails);
      this._QualitycontrolService.updateQuality(saveqc).subscribe(response => {
        if (response) {
          this.model = {};
          this.model1 = {};
          this.QCdetails = [];
          this.dataSource = new MatTableDataSource(this.QCdetails);
          this.getQuality();
          this._toastr.success('Quality Control updated Successfully')
          this.editFlag = !this.editFlag;
        }
      });
    } else {
      saveqc.QCDetails = JSON.stringify(arrQCDetails);
      this._QualitycontrolService.insertQuality(saveqc).subscribe(response => {
        if (response) {
          this.model = {};
          this.model1 = {};
          this.QCdetails = [];
          this.dataSource = new MatTableDataSource(this.QCdetails);
          this.getQuality();
          this._toastr.success('Quality Control added Successfully')
          this.editFlag = !this.editFlag;
        }
      });
    }
  }

  ngOnInit() {

    this.getQuality();
    this.Getdropdown();
    this.getProduct();
  }
  getProduct() {
    this._ProductService.getProduct().subscribe(response => {
      response['result'];
      if (response) {
        this.isLoading = true;
        this.product = response;
      }
    });
  }
  handleClick(event) {
    event.stopPropagation();
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();         // Remove whitespace
    filterValue = filterValue.toLowerCase();  // MatTableDataSource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }

  UpdateQuality(value) {

    // this.Getdropdown();
    // this.show = !this.show;
    this.value = value;
    this.isEditing = true;
    this.editFlag = true;
    this.GetQCList(value.QCID);
  }


  Cancel() {
    debugger;
    this.show = !this.show;
    this.isEditing = false;

  }
  getQuality() {
    const id = 0;
    this._QualitycontrolService.getQCList(id).subscribe(response => {
      debugger;
      response['result'];
      if (response) {
        this.isLoading = true;
        this.QualityCheckList = response;
        this.dataSource = new MatTableDataSource(this.QualityCheckList.QCCheckMaster);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;

      }
    });
  }

  GetQCList(QCId) {
    debugger;
    this._QualitycontrolService.getQCList(QCId).subscribe(response => {
      debugger;
      response['result'];
      if (response) {
        const data = response;
        this.model = data.QCCheckMaster[0];
        this.datasourceone = data.QCCheckDetails;
        this.QCdetails = this.datasourceone;
      }
    });
  }

  Getdropdown() {
    debugger;
    // this._QualitycontrolService.getdropdown().subscribe(response => {
    //   response['result'];
    //   debugger;
    //   if (response) {
    //     this.ddvalues = response;
    //     console.log(this.ddvalues);
    //   }
    // })
  }

  DeleteQuality(element) {
    debugger;
    if (confirm("Are you sure?")) {
      this._QualitycontrolService.deleteQuality(element.QCID).subscribe((response: any) => {
        debugger;
        if (response) {
          alert("successfully deleted");
          this.QCdetails.Table = []
          this.QCdetails.Table = []
          this.Cancel();
          this.ngOnInit();


        }
      });
    }

  }

  addQCField(model1) {
    debugger;
    // model1.ChkID = parseInt(model1.ChkID);
    // model1.prdName = (model1.prdName);
    model1.ChkName = (model1.ChkName);
    this.setQcRow(model1);
  }
  setQcRow(model1) {
    debugger;
    this.checkFlag = false;
    if (this.Isediting == true) {

      if (this.index != undefined) {
        this.QCdetails[this.index] = model1;
      } else {
        this.QCdetails.push(model1);
      }

    } else {
      this.QCdetails.push(model1);
    }

    this.model1 = {};
    this.datasourceone = new MatTableDataSource(this.QCdetails);
    console.log(this.datasourceone)
    this.Isediting = false;
  }
  updateQCLdetails(element, Isediting, index) {
    debugger;
    this.checkFlag = true;
    this.Isediting = Isediting;
    this.model1 = element;
    this.index = index;
    console.log(element, this.model);
  }


  deleteQCLdetails(index) {

    this.QCdetails.splice(index, 1);
    this.datasourceone = new MatTableDataSource(this.QCdetails);
  }

  addQc() {
    this.editFlag = true;
    this.model = {};
    this.model1 = [];
    this.datasourceone.filteredData = [];
    this.QCdetails = [];
  }

  backQCListForm() {
    this.editFlag = false;
  }
  datavalue: any
  openQcDetails(dataVal) {
    this.GetQCList(dataVal.QCID);
    this.datavalue = dataVal;
    this._dialog.open(this.qCDialog, {
      panelClass: 'Dialog',
      width: '800px',
    })


  }
  closeDialog() {
    this._dialog.closeAll();
  }
}
