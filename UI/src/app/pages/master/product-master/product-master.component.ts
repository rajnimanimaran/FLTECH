import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { HttpClient } from '@angular/common/http';
import { Product, UOMMaster } from 'src/app/shared/models/product';
import { ProductService } from 'src/app/shared/services/master-services/product.service';
import { ToastrService } from 'ngx-toastr';
import { UOMService } from 'src/app/shared/services/master-services/uom.service';

@Component({
  selector: 'app-product-master',
  templateUrl: './product-master.component.html',
  styleUrls: ['./product-master.component.scss']
})
export class ProductMasterComponent implements OnInit {
  public show: boolean = false;
  isEditing: boolean = false;
  selection = new SelectionModel<Element>(true, []);
  public ProductForm: FormGroup;
  API_URL: string;
  model: Product = new Product();
  product: Product[] = [];
  @ViewChild('productDialog') productDialog: TemplateRef<any>;
  UOMData: UOMMaster[] = [];
  editFlag: string = '';
  isLoading: boolean;



  constructor(public form: FormBuilder, public _dialog: MatDialog,
    private _uomservice:UOMService, private _http: HttpClient, private _ProductService: ProductService,
    private _toastr: ToastrService) {
  }

  dataSource = new MatTableDataSource();
  displayedColumns = ['SNo', 'ProductCode', 'ProductName', 'UOM', 'Stock', 'ReOrderlevel', 'action'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  saveProduct() {
    if (this.editFlag === 'save') {
      this._ProductService.insertProduct(this.model).subscribe(response => {
        if (response == true) {
          debugger
          this.ProductForm.reset();
          this._dialog.closeAll();
          this._toastr.success('Product added Successfully!')
        }
      });
    }
    if (this.editFlag === 'update') {
      this._ProductService.updateProduct(this.model).subscribe(response => {
        if (response == true) {
          this.ProductForm.reset();
          this._dialog.closeAll();
          this._toastr.success('Product updated Successfully!')
        }
      })
    }
  }

  updateProducts(element) {
    this.addproduct();
    this.editFlag = 'update';
    this.model = element;
  }


  getProduct() {
    this._ProductService.getProduct().subscribe(response => {
      response['result'];
      if (response) {
        this.isLoading = true;
        this.product = response;
        this.dataSource = new MatTableDataSource(this.product);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }
    });
  }

  ngOnInit() {
     this.buildForm();
    this.getProduct();
    this.getUOMMaster();

  }

  getUOMMaster() {
    this._uomservice.get().subscribe(response => {
      response['result'];
      if (response) {
        this.UOMData = response
      }
    })
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
      prdCode: ['', [Validators.required]],
      prdName: ['', [Validators.required]],
      uom: ['', [Validators.required]],
      stock: ['', [Validators.required]],
      sell_price: ['', [Validators.required]],
      cost_price: ['', [Validators.required]],
      HSNCode: ['', [Validators.required]],
      re_Orderlevel: ['', [Validators.required]],
    });
  }
  addproduct() {
    this.editFlag = 'save';
    const dialogRef = this._dialog.open(this.productDialog, {
      panelClass: 'Dialog',
      //hasBackdrop: false
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getProduct();
    })
  }
  closeDialog() {
    this.ProductForm.reset();
    this._dialog.closeAll();
  }
}


