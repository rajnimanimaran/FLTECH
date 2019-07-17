import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Contractor } from 'src/app/shared/models/contractors';
import { SubContractService } from 'src/app/shared/services/master-services/contractor.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-contractor-master',
  templateUrl: './contractor-master.component.html',
  styleUrls: ['./contractor-master.component.scss']
})
export class ContractorMasterComponent implements OnInit {
  public show: boolean = false;
  isEditing: boolean = false;
  selection = new SelectionModel<Element>(true, []);
  public ProductForm: FormGroup;
  API_URL: string;
 model: Contractor = new Contractor();
  @ViewChild('ContractorDialog') ContractorDialog: TemplateRef<any>;
  // {
  //   subContID: 0,
  //   Name: "",
  //   Address: "",
  //   GstNo: "",
  //   contactNumber: 0,
  //   supplierID: 0,
  //   emailID: "",
  //   IsActive: false
  // }
  contract: Contractor[] = [];
  isLoading: boolean;

  constructor(public form: FormBuilder, public _dialog: MatDialog, private _http: HttpClient, 
    private _subContractService: SubContractService,private _toastr: ToastrService,
    ) {
    this.API_URL = environment.apiUrl;
  }
  editFlag: string;
  dataSource = new MatTableDataSource();
  displayedColumns = ['SNo', 'supplierID', 'Name', 'Address', 'GstNo', 'contactNumber', 'emailID', 'action'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  saveContract() {
    debugger;
    if (this.editFlag === 'save') {
      this._subContractService.insertsubcontract(this.model).subscribe(response => {
        if (response) {
          // this.CustomerForm.reset();
          this._dialog.closeAll();
          this._toastr.success('Contractor added Successfully!')
        }
      });
    }
    else if(this.editFlag === 'update') {
      this._subContractService.updatesubcontract(this.model).subscribe(response => {
        if (response) {
          // this.CustomerForm.reset();
          this._dialog.closeAll();
          this._toastr.success('Customer updated Successfully!')
        }
      })
    }
   
  }

  updateContract(element, isEditing) {
    this.addContractor();
    this.editFlag = 'update';
    this.model = element;
  }

  getContract() {
    this._subContractService.getsubContract().subscribe(response => {
      if (response) {
        this.isLoading = true;
        this.contract = response
        this.dataSource = new MatTableDataSource(this.contract);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        console.log(this.contract)
      }
    });
  }



  ngOnInit() {
    this.getContract();
  }


  ngAfterViewInit() {

    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
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
      supplierID: ['', [Validators.required]],
      Name: ['', [Validators.required]],
      Address: ['', [Validators.required]],
      GstNo: ['', [Validators.required]],
      contactNumber: ['', [Validators.required]],
      emailID: ['', [Validators.required]],


    });
    console.log(this.ProductForm)
  }
  addContractor(){
    this.editFlag = 'save'
  const dialogRef =  this._dialog.open(this.ContractorDialog,{
      panelClass:'Dialog',
     hasBackdrop:false
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getContract();
    })
  }
  closeDialog(){
    this._dialog.closeAll();
  }
}
