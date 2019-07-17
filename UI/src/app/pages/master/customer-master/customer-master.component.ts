import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatTableDataSource, MatSort, MatPaginator, MatDialog } from '@angular/material';
import { HttpClient } from '@angular/common/http';
import { Customers } from 'src/app/shared/models/customer';
import { environment } from 'src/environments/environment';
import { CustomerService } from 'src/app/shared/services/master-services/customer.service';
import { CountryService } from 'src/app/shared/services/adminstrator/location/country.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-customer-master',
  templateUrl: './customer-master.component.html',
  styleUrls: ['./customer-master.component.scss']
})
export class CustomerMasterComponent implements OnInit {


  customer: Customers[] = [];
  public show: boolean = false;
  isEditing: boolean = false;
  selection = new SelectionModel<Element>(true, []);
  public CustomerForm: FormGroup;
  API_URL: string;
  //  model:Customers = new Customers();
  model: any = {}
  CountryIdName: any = []
  StateIdName: any = []
  stateData: any = [];
  cityData: any = [];
  @ViewChild('customerDialog') customerDialog: TemplateRef<any>;
  editFlag: string;
  isLoading: boolean;
  constructor(public form: FormBuilder, private _http: HttpClient, public _dialog: MatDialog,
    private _customerService: CustomerService, private _toastr: ToastrService,
    private _countryServices: CountryService
  ) {
    this.buildForm();
  }

  dataSource = new MatTableDataSource();
  displayedColumns = ['SNo', 'CustomerCode', 'CustomerName', 'CustomerAddress', 'CustomerContact', 'CustomerEmail', 'GSTNo', 'action'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  saveCustomer() {
    if (this.editFlag === 'save') {
      this._customerService.insertCustomer(this.model).subscribe(response => {
        if (response == true) {
          this.CustomerForm.reset();
          this._dialog.closeAll();
          this._toastr.success('Customer added Successfully!')
        }
      });
    }
    if (this.editFlag === 'update') {
      debugger;
      this._customerService.updateCustomer(this.model).subscribe(response => {
        debugger;
        if (response == true) {
          this.CustomerForm.reset();
          this._dialog.closeAll();
          this._toastr.success('Customer updated Successfully!')
        }
      })
    }
  }



  updateCustomers(element) {
    this.addcustomer();
    this.editFlag = 'update';
    this.model = element;
    this.model.CustCountry = Number(element.CustCountry);
    this.model.CustState = Number(element.CustState);
    this.model.CustCity = Number(element.CustCity);

  }



  getCustomer() {
    this._customerService.getCustomer().subscribe(response => {
      if (response) {
        this.isLoading = true;
        this.customer = response;
        this.dataSource = new MatTableDataSource(this.customer);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        console.log(this.customer)
      }
    });
  }


  GetCountryName() {
    debugger;
    this._countryServices.listCountryDetails().subscribe((data: any) => {
      if (data) {
        this.CountryIdName = JSON.parse(data._body);
        console.log(this.CountryIdName, 'CountryName')
      }
    }
    )
  }

  ngOnInit() {
    this.getCustomer();
    this.GetCountryName();
  }

  getStateName(Id) {
    this._customerService.getState(Id).subscribe((data: any) => {
      if (data) {
        this.stateData = data;
      }
    });
  }
  getCityName(Id) {
    this._customerService.getCity(Id).subscribe((data: any) => {
      if (data) {
        this.cityData = data;
      }
    });
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
    this.CustomerForm = this.form.group({
      CustCode: ['', [Validators.required]],
      CustName: ['', [Validators.required]],
      CustAddress: ['', [Validators.required]],
      CustContact: ['', [Validators.required]],
      CustEmail: ['', [Validators.required]],
      GSTNo: ['', [Validators.required]],
      CountryName: ['', [Validators.required]],
      StateName: ['', [Validators.required]],
      CityName: ['', [Validators.required]],
    });
  }

  addcustomer() {
    this.editFlag = 'save'
    const dialogRef = this._dialog.open(this.customerDialog, {
      panelClass: 'Dialog',
      hasBackdrop: false
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getCustomer();
    })
  }

  closeDialog() {
    this._dialog.closeAll();
    this.CustomerForm.reset();
  }
}
