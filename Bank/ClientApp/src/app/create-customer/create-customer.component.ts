import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router'; 
import { CustomerService } from '../Services/customer.service' 
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Http, Response } from '@angular/http';


@Component({
  selector: 'app-create-customer',
  templateUrl: './create-customer.component.html',
  styleUrls: ['./create-customer.component.css']
})
export class CreateCustomerComponent implements OnInit {

  customerForm: FormGroup;
  title: string = "Create";
  employeeId: number;
  errorMessage: any;
  cityList: Array<any> = [];

  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
    private _customerService: CustomerService, private _router: Router) {
    if (this._avRoute.snapshot.params["id"]) {
      this.employeeId = this._avRoute.snapshot.params["id"];
    }

    this.customerForm = this._fb.group({
      customerId: 0,
      name: ['', [Validators.required]],
      gender: ['', [Validators.required]],
      department: ['', [Validators.required]],
      city: ['', [Validators.required]]
    })
  }

  ngOnInit() {

    //this._customerService.getCityList().subscribe(
    //  data => this.cityList = data
    //)

    if (this.employeeId > 0) {
      this.title = "Edit";
      this._customerService.getCustomerById(this.employeeId)
        .subscribe(resp => this.customerForm.setValue(resp)
          , error => this.errorMessage = error);
    }

  }

  save() {

    if (!this.customerForm.valid) {
      return;
    }

    if (this.title == "Create") {
      this._customerService.saveCustomer(this.customerForm.value)
        .subscribe((data) => {
          this._router.navigate(['/fetch-employee']);
        }, error => this.errorMessage = error)
    }
    else if (this.title == "Edit") {
      this._customerService.updateCustomer(this.customerForm.value)
        .subscribe((data) => {
          this._router.navigate(['/fetch-employee']);
        }, error => this.errorMessage = error)
    }
  }

  cancel() {
    this._router.navigate(['/fetch-employee']);
  }

  get name() { return this.customerForm.get('name'); }
  get gender() { return this.customerForm.get('gender'); }
  get department() { return this.customerForm.get('department'); }
  get city() { return this.customerForm.get('city'); }  


}
