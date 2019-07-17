import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { SelectionModel } from '@angular/cdk/collections';
import { DatePipe } from '@angular/common';
import { UOMMaster, ProductMaster } from 'src/app/shared/models/product';
import { ProductService } from 'src/app/shared/services/master-services/product.service';
import { environment } from 'src/environments/environment';
import { SupplierService } from 'src/app/shared/services/master-services/supplier.service';
import { RawMaterialService } from 'src/app/shared/services/master-services/rawMaterial.service';
import { SalesOrderMaster } from 'src/app/shared/models/sales-order';
import { SalesOrderMasterService } from 'src/app/shared/services/sales-order.service';
import { FormGroup, Validators } from '@angular/forms';
import { CustomerService } from 'src/app/shared/services/master-services/customer.service';
import { UOMService } from 'src/app/shared/services/master-services/uom.service';
import { AppComponent } from 'src/app/app.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-sales-order',
  templateUrl: './sales-order.component.html',
  styleUrls: ['./sales-order.component.scss'],
  providers: [DatePipe]
})
export class SalesOrderComponent implements OnInit {
  @ViewChild('SODialog') SODialog: TemplateRef<any>;
  categoryModel: any = [];
  show = false;
  model: any = {}
  field: any = [];
  isEditing: boolean;
  filePath: any;
  OrderFormDetail: any = []
  model1: any = {};
  vendornameview: any
  vendordetaildisplay: any[]
  Productnameview: any = []
  HSN_CodeDisplay: any = []
  ProductHSN_Codeview: any;
  Productsizeview: any = []
  API_URL: string;
  public edit: any;
  public ShowEditRow: boolean = false;
  public buttonName: any = 'edit';
  arrayInventry: any = [];
  AlltotalAmount: any;
  count: number;
  UOMData: UOMMaster[] = [];
  productData: ProductMaster[] = [];
  // SupplierName: getSupplierName[] = [];

  getSalesData: any = [];
  salesOrderData = new MatTableDataSource<any>(this.getSalesData);
  dataSource = new MatTableDataSource<any>(this.OrderFormDetail);
  displayedgridcolumns = ['SNo', 'SName', 'OrderDate', 'ACKDate', 'action']
  displayedColumns = ['SNo', 'ProductName', 'quantity', 'UOM', 'Rate', 'Tax', 'Amount', 'TAmount', 'action'];
  index: any;
  editflag: boolean;
  value: boolean;
  editFlag: string;
  SalesOrderForm: FormGroup;
  isLoading: boolean;
  constructor(private _toaster: ToastrService,
    private _ProductService: ProductService,
    private _supplierService: SupplierService,
    private _RawMaterialService: RawMaterialService,
    private _customerService: CustomerService, private _uomservice: UOMService,
    private _salesorderservice: SalesOrderMasterService,
    private _datePipe: DatePipe, private _dialog: MatDialog,
    private form: FormBuilder
  ) {
    this.API_URL = environment.apiUrl;
    this.buildForm();
  }

  public buildForm() {
    this.SalesOrderForm = this.form.group({
      CustId: ['', [Validators.required]],
      OrderDate: ['', [Validators.required]],
      ACKDate: ['', [Validators.required]],
      AssignedTo: ['', [Validators.required]],
      materialType: ['', [Validators.required]],
      OrderStatus: ['', [Validators.required]],
      OrderTakenBy: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    this.getUOMMaster();
    const id = 0;
    this.getSalesOrderDetails(id);
    this.getProduct();
    // this.getRawmaterial();
    // this.getSupplier();
    this.getCustomer();

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
    debugger;
    if (model1.prdID) {
      const x = this.Product.filter(x => x.prdID == model1.prdID);
      model1.prdName = x[0].prdName;
    }
    if (model1.RMID) {
      const x = this.Rawmaterial.filter(x => x.RMID == model1.RMID);
      model1.RMName = x[0].RMName;
    }
    // model1.GSTNo = parseInt(model1.GSTNo);
    model1.HSNCode = model1.HSNCode;
    model1.OrderItemID = 1;
    model1.Quantity = parseInt(model1.Quantity);
    model1.Rate = parseFloat(model1.Rate);
    model1.Tax = parseFloat(model1.Tax);
    model1.Amount = (parseInt(model1.Quantity) * parseFloat(model1.Rate));
    model1.Amount = parseFloat(model1.Amount);
    model1.T_Amount = (parseFloat(model1.Amount) * (parseFloat(model1.Tax) / 100)) + parseFloat(model1.Amount)
    model1.T_Amount = parseFloat(model1.T_Amount);
    model1.BalanceQty = model1.Quantity;
    model1.AssignedQty = 0;
    model1.AlreadyAssignedQty = 0;
    model1.WorkStatus = 0;
    this.setProductAndUOMName(model1);
  }

  setProductAndUOMName(model1) {
    debugger;
    if (this.isEditing == true) {
      for (var i in this.UOMData) {
        if (this.UOMData[i].UOMID == model1.UOMID) {
          model1.UOMName = this.UOMData[i].UOMName;
        }
      } for (var x in this.productData) {
        if (this.productData[x].prdID == model1.prdID) {
          model1.prdName = this.productData[x].prdName;
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
      } for (var x in this.productData) {
        if (this.productData[x].prdID == model1.prdID) {
          model1.prdName = this.productData[x].prdName;
        }
      }
      this.OrderFormDetail.push(model1);
    }
    this.model1 = {};
    this.dataSource = new MatTableDataSource(this.OrderFormDetail);
    console.log(this.dataSource)
    // this.isEditing = false;
    // this.editflag = false;
  }


  saveOrderForm() {
    debugger;

    const orderForm = new SalesOrderMaster();
    const arrOrderFormDetail = [];
    for (var start in this.OrderFormDetail) {
      if (this.OrderFormDetail[start].Quantity != undefined) {
        arrOrderFormDetail.push(this.OrderFormDetail[start]);
      }
    }


    orderForm.CustId = this.model.CustId;
    orderForm.prdID = this.model1.prdID
    orderForm.materialType = 2;
    orderForm.AssignedQty = 0;
    orderForm.AlreadyAssignedQty = 0;
    orderForm.BalanceQty = this.model1.Quantity;
    orderForm.WorkStatus = 1
    orderForm.OrderDate = new Date(this._datePipe.transform(this.model.OrderDate, 'yyyy-MM-dd'));
    orderForm.ACKDate = new Date(this._datePipe.transform(this.model.ACKDate, 'yyyy-MM-dd'));
    orderForm.AssignedTo = '1'// this.model.AssignedTo;
    orderForm.OrderTakenBy = '1' //this.model.OrderTakenBy;
    orderForm.OrderStatus = 1 // this.model.OrderStatus;

    if (this.editFlag == 'update') {
      orderForm.OrderID = this.model.OrderID;
      orderForm.OrderDetails = JSON.stringify(this.OrderFormDetail);
      this._salesorderservice.updateSalesOrder(orderForm).subscribe(response => {
        if (response) {
          this.model = {};
          this.model1 = {};
          this.OrderFormDetail = [];
          this.dataSource = new MatTableDataSource(this.OrderFormDetail);
          const obj = 0;
          this.getSalesOrderDetails(obj);
          this.show = !this.show;
        }
      });
    } if (this.editFlag == 'add') {
      orderForm.OrderDetails = JSON.stringify(arrOrderFormDetail);
      this._salesorderservice.insertSalesOrder(orderForm).subscribe(response => {
        if (response) {
          this.model = {};
          this.model1 = {};
          this.OrderFormDetail = [];
          this.dataSource = new MatTableDataSource(this.OrderFormDetail);
          const obj = 0
          this.getSalesOrderDetails(obj);
          this._toaster.info("Your Order Number is"+" "+(this.getSalesData[0].OrderID + 1))
          this.show = !this.show;
        }
      });
    }

  }



  getSalesOrderDetails(id) {
    debugger;
    this._salesorderservice.getSalesOrder(id).subscribe(response => {
      if (response && id > 0) {
        this.model = response.SalesOrderMaster[0];
        this.OrderFormDetail = response.SalesOrderDetails;
        this.salesOrderData = new MatTableDataSource(this.getSalesData);
        this.dataSource = new MatTableDataSource(this.OrderFormDetail);
      }
      else {
        this.isLoading = true;
        this.getSalesData = response.SalesOrderMaster;
        this.salesOrderData = new MatTableDataSource(this.getSalesData);
        alert
      }
    });
  }

  editsalesDetails(element, isEditing, index) {

    this.isEditing = isEditing;
    this.model1 = element;
    this.index = index;
  }

  updatesalesDetailsgrid(value) {
    this.editFlag = 'update';
    this.show = true;
    this.value = value;
    this.isEditing = true;
    this.getSalesOrderDetails(value.OrderID);
  }

  deleteSalesDetails(index) {
    this.OrderFormDetail.splice(index, 1);
    this.dataSource = new MatTableDataSource(this.OrderFormDetail);
    this.model1 = [];
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

  initialising() {
    this.OrderFormDetail = [{ isAdd: true, isDelete: false }];
    for (var i in this.OrderFormDetail) {
      this.OrderFormDetail[i].BillNo = this.model.BillNo
    }
  }

  assignTableBillno() {

    for (var i in this.OrderFormDetail) {
      this.OrderFormDetail[i].BillNo = this.model.BillNo;
    }
  }


  cancel() {
    this.model = {};
    this.model1 = {};
    this.OrderFormDetail = [];
    this.vendordetaildisplay = [];
  }
  datavalue: any;
  openSODetails(data) {
    this.getSalesOrderDetails(data.OrderID);
    this.datavalue = data;
    this._dialog.open(this.SODialog, {
      panelClass: 'Dialog',
      width: '800px',
    })
  }
  closeDialog() {
    this._dialog.closeAll();
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
    return this.OrderFormDetail.map(t => t.T_Amount).reduce((acc, value) => acc + value, 0);
  }


  private newAttribute: any = {};



  editCategory(element, value) {
    //this.isEditing = element
    this.show = element;
    this.categoryModel = value;
  }
  AddproductCategorydetails() {
    this.SalesOrderForm.reset();
    this.model = [];
    this.editFlag = 'add';
    this.categoryModel = {};
    this.show = true;
    this.OrderFormDetail = [];
    this.dataSource = new MatTableDataSource<Element>(this.OrderFormDetail);

    //this.isEditing = false
  }
  backproductCategoryListForm() {
    this.show = false;
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

  Supplier = [];
  getSupplier() {
    this._supplierService.getQuality().subscribe(response => {
      if (response) {
        this.Supplier = response;
      }

    })

  }
  Customer = [];
  getCustomer() {
    this._customerService.getCustomer().subscribe(response => {
      if (response) {
        this.Customer = response;
        console.log(this.Customer, 'Cusotmer')
      }
    });
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


