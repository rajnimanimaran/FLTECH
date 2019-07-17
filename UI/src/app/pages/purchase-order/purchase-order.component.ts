import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { HttpClient } from '@angular/common/http';
import { SelectionModel } from '@angular/cdk/collections';
import { DatePipe } from '@angular/common';
import { UOMMaster } from 'src/app/shared/models/product';
import { rmName } from 'src/app/shared/models/rawmaterial';
import { ProductService } from 'src/app/shared/services/master-services/product.service';
import { RawMaterialService } from 'src/app/shared/services/master-services/rawMaterial.service';
import { PurchaseOrder } from 'src/app/shared/models/purchaseorder';
import { PurchaseOrderService } from 'src/app/shared/services/purchase-order.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SupplierService } from 'src/app/shared/services/master-services/supplier.service';
import { UOMService } from 'src/app/shared/services/master-services/uom.service';

@Component({
  selector: 'app-purchase-order',
  templateUrl: './purchase-order.component.html',
  styleUrls: ['./purchase-order.component.scss'],
  providers: [DatePipe]
})
export class PurchaseOrderComponent implements OnInit {
  show = false;
  filePath: any;
  getPODetails = []
  model: any = {};
  model1: any = {};
  materialID = 1;
  displayedgridcolumnsgrid = ['SNo', 'Code', 'SName', 'PurchaseDate', 'ExpectDeliveryDate', 'action']
  displayedColumns = ['SNo', 'RMID', 'quantity', 'UOM', 'HSNCode', 'Rate', 'Tax', 'Amount', 'TAmount', 'action'];
  OrderFormDetail: any = [];
  dataSource = new MatTableDataSource<Element>(this.OrderFormDetail);
  isEditing: boolean;
  index: any;
  editflag: boolean;
  UOMData: UOMMaster[] = [];
  Name: rmName[] = [];
  getpurchaseData = [];
  getpurchase = new MatTableDataSource<Element>(this.getpurchaseData);
  // SupplierName: getSupplierName[] = [];
  value: any;
  API_URL: string;
  // models1:GetreportID[] = [];
  modelss: any = {};
  PurchaseOrderForm: FormGroup;
  disableRadioButton: boolean;
  isLoading: boolean;
  //  control=false;
  constructor(private http: HttpClient, private _uomservice: UOMService,
    private _PurchaseOrderService: PurchaseOrderService,
    private _supplierService: SupplierService,
    private _ProductService: ProductService,
    private _RawMaterialService: RawMaterialService,
    private _datePipe: DatePipe, public form: FormBuilder,
    public dialog: MatDialog) {
    this.buildForm();
  }
  public buildForm() {
    this.PurchaseOrderForm = this.form.group({
      PurchaseID: ['', [Validators.required]],
      Code: ['', [Validators.required]],
      SID: ['', [Validators.required]],
      PurchaseDate: ['', [Validators.required]],
      ExpectDeliveryDate: ['', [Validators.required]],
      materialType: ['', [Validators.required]],
    });
  }
  ngOnInit() {
    this.getUOMMaster();
    const PurchaseId = 0;
    this.getPurchaseOrderDetails(PurchaseId, this.materialID);
    this.getSupplier();
    this.getRawmaterial();
    this.getProduct();

  }
  getUOMMaster() {
    this._uomservice.get().subscribe(response => {
      response['result'];
      if (response) {
        this.UOMData = response
      }
    })
  }









  addFieldValue(model1) {
    this.disableRadioButton = true;
    // model1.ProductName = model1.ProductNameId.ProductName;
    // model1.ProductNameId = model1.ProductNameId.ProductNameId;
    //model1.UOMID = model1.ProductSizeId.UOMID;
    if (model1.prdID) {
      const x = this.Product.filter(x => x.prdID == model1.prdID);
      model1.prdName = x[0].prdName;
    }
    if (model1.RMID) {
      const x = this.Rawmaterial.filter(x => x.RMID == model1.RMID);
      model1.RMName = x[0].RMName;
    }


    model1.RMName = model1.RMName;
    if (model1.GSTNO) {
      model1.GSTNo = parseInt(model1.GSTNo);
    }
    model1.MID = this.model.materialType;
    model1.Quantity = parseInt(model1.Quantity);
    model1.Rate = parseFloat(model1.Rate);
    model1.Tax = parseFloat(model1.Tax);
    model1.Amount = (parseInt(model1.Quantity) * parseFloat(model1.Rate));
    model1.Amount = parseFloat(model1.Amount);
    model1.TotalAmount = (parseFloat(model1.Amount) * (parseFloat(model1.Tax) / 100)) + parseFloat(model1.Amount)
    model1.T_Amount = parseFloat(model1.TotalAmount);
    this.setProductAndUOMName(model1);
  }

  setProductAndUOMName(model1) {
    debugger;
    if (this.isEditing == true) {
      for (var i in this.UOMData) {
        if (this.UOMData[i].UOMID == model1.UOMID) {
          model1.UOMName = this.UOMData[i].UOMName;
        }
      } for (var x in this.Name) {
        if (this.Name[x].RMID == model1.RMID) {
          model1.RMName = this.Name[x].RMName;
        }
      }
      if (this.index != undefined) {
        this.OrderFormDetail[this.index] = model1;
      } else {
        this.OrderFormDetail.push(model1);
      }
    } else {
      // this.OrderFormDetail.push(model1);
      for (var i in this.UOMData) {
        if (this.UOMData[i].UOMID == model1.UOMID) {
          model1.UOMName = (this.UOMData[i].UOMName);
        }
      } for (var x in this.Name) {
        if (this.Name[x].RMID == model1.RMID) {
          model1.RMName = (this.Name[x].RMName);
        }
      }
      this.OrderFormDetail.push(model1);
    }
    this.model1 = {};
    this.dataSource = new MatTableDataSource(this.OrderFormDetail);
    console.log(this.dataSource)

  }


  saveOrderForm() {

    const orderForm = new PurchaseOrder();
    const arrOrderFormDetail = [];
    for (var start in this.OrderFormDetail) {
      if (this.OrderFormDetail[start].Quantity != undefined) {
        arrOrderFormDetail.push(this.OrderFormDetail[start]);
      }
    }
    debugger;
    orderForm.PurchaseID = this.model.PurchaseID;
    orderForm.Code = this.model.code;
    orderForm.SupplierID = this.model.SID;
    orderForm.PurchaseDate = new Date(this._datePipe.transform(this.model.PurchaseDate, 'yyyy-MM-dd'));
    orderForm.ExpectDeliveryDate = new Date(this._datePipe.transform(this.model.ExpectDeliveryDate, 'yyyy-MM-dd'));
    orderForm.MaterialType = this.model.materialType;
    if (this.editFlag === 'update') {
      orderForm.OrderDetails = JSON.stringify(this.OrderFormDetail);
      this._PurchaseOrderService.updatePurchaseOrder(orderForm).subscribe(response => {
        if (response == true) {
          this.model = {};
          this.model1 = {};
          this.OrderFormDetail = [];
          this.dataSource = new MatTableDataSource(this.OrderFormDetail);
          const PurchaseId = 0;
          this.getPurchaseOrderDetails(PurchaseId, this.materialID);
          this.show = !this.show;
        }
      });
    }
    if (this.editFlag === 'add') {
      orderForm.OrderDetails = JSON.stringify(arrOrderFormDetail);
      this._PurchaseOrderService.insertPurchaseOrder(orderForm).subscribe(response => {
        if (response == true) {
          this.model = {};
          this.model1 = {};
          this.OrderFormDetail = [];
          this.dataSource = new MatTableDataSource(this.OrderFormDetail);
          const PurchaseId = 0;
          this.getPurchaseOrderDetails(PurchaseId, this.materialID);
          this.show = !this.show;
        }
      });
    }

  }



  selection = new SelectionModel<Element>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  userCompanyId: number;


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
  editFlag: string = '';
  AddproductCategorydetails() {
    this.editFlag = 'add';
    this.PurchaseOrderForm.reset();
    this.OrderFormDetail = [];
    this.dataSource = new MatTableDataSource<Element>(this.OrderFormDetail);
    this.show = true;

  }
  backproductCategoryListForm() {
    this.show = false;
  }
  removeField() {
    this.model1 = []
  }
  editPurchaseDetails(element, isEditing, index) {
    if (element.prdID) {
      this.materialType = false;
    }
    if (element.RMID) {
      this.materialType = true;
    }
    this.isEditing = isEditing;
    this.model1 = element;
    this.index = index;
    // this.editflag = true;

  }

  updatePurchaseDetailsgrid(value) {
    debugger
    this.editFlag = 'update';
    this.show = true;
    this.value = value;
    this.isEditing = true;
    //  this.getPODetails(value.PurchaseID);
    this.getPurchaseOrderDetails(value.PurchaseID, this.materialID)
  }





  deletePurchaseDetails(index) {

    this.OrderFormDetail.splice(index, 1);
    this.dataSource = new MatTableDataSource(this.OrderFormDetail);
    this.model1 = [];
  }

  getPurchaseOrderDetails(PurchaseID, materialID) {

    this._PurchaseOrderService.getPurchaseOrder(PurchaseID, materialID).subscribe(response => {
      // response['result'];
      debugger;
      if (response && PurchaseID === 0) {
        this.isLoading = true;
        this.getpurchaseData = response.PurchaseOrderMaster;
        this.getpurchase = new MatTableDataSource(this.getpurchaseData);
      }
      else if (response && PurchaseID > 0) {
        this.isLoading = true;
        this.model = response.PurchaseOrderMaster[0];
        this.getPODetails = response.PurchaseOrderDetails;
        this.getpurchase = new MatTableDataSource(this.getpurchaseData);
        this.dataSource = new MatTableDataSource(this.getPODetails);
      }
    });
  }

  convertToInt(val) {
    if (!isNaN(val) && (val != undefined) && (val != ''))
      return parseInt(val);
    else { return 0 }
  }
  //convert to float
  convertToFloat(val) {
    if (!isNaN(val) && (val != undefined) && (val != ''))
      return parseFloat(val);
    else { return 0 }
  }

  getTotal = function () {
    var total = 0;
    for (var i in this.OrderFormDetail) {
      if (this.OrderFormDetail[i].Quantity) {
        total += (this.OrderFormDetail[i].Quantity * 1);
      }
    }
    if (!isNaN(total) && (total != undefined))
      return total;
    else { return 0 }
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
  getAmount = function () {
    var total = 0;
    for (var i in this.OrderFormDetail) {
      if (this.OrderFormDetail[i].Quantity && this.OrderFormDetail[i].Rate) {
        total += (this.OrderFormDetail[i].Quantity * this.OrderFormDetail[i].Rate);
      }
    }
    if (!isNaN(total) && (total != undefined))
      return total;
    else { return 0 }
  }

  getTotalAmount = function () {
    var total = 0;
    for (var i in this.OrderFormDetail) {
      if (this.OrderFormDetail[i].Quantity && this.OrderFormDetail[i].Rate) {
        total += (((this.OrderFormDetail[i].Quantity * this.OrderFormDetail[i].Rate)) * 1) +
          ((this.OrderFormDetail[i].Quantity * this.OrderFormDetail[i].Rate) * (this.OrderFormDetail[i].Tax / 100 * 1));
      }
    }
    if (!isNaN(total) && (total != undefined))
      return total;
    else { return 0 }
  }
  getGrandtotal() {
    return this.OrderFormDetail.map(t => t.TotalAmount).reduce((acc, value) => acc + value, 0);
  }


  private newAttribute: any = {};

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
  materialType: boolean = false;
  selectType(type: string) {
    if (type === 'RM') {
      this.materialType = true
    }
    if (type === 'PRD') {
      this.materialType = !this.show
    }
  }
  Supplier = [];
  getSupplier() {
    this._supplierService.getQuality().subscribe(response => {
      if (response) {
        this.Supplier = response;
      }

    })

  }
  Product = [];
  getProduct() {
    this._ProductService.getProduct().subscribe(response => {
      if (response) {
        this.Product = response;
      }

    })

  }
  Rawmaterial = [];
  getRawmaterial() {
    this._RawMaterialService.getRawmaterial().subscribe(response => {
      if (response) {
        this.Rawmaterial = response;
      }

    })

  }
}