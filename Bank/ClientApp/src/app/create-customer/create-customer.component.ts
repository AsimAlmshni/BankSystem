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
  title: string = 'Create';
  customerId: number;
  customerName: string;
  mainAccountNuumber: string;
  mainCurrency: string;
  totalBalance: number;

  errorMessage: any;

  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
    private _customerService: CustomerService, private _router: Router) {
    if (this._avRoute.snapshot.params['id']) {
      this.customerId = this._avRoute.snapshot.params['id'];
    }

    this.customerForm = this._fb.group({
      customerId: 0,
      customerName: ['', [Validators.required]],
      mainAccountNumber: ['', [Validators.required]],
      mainCurrency: ['', [Validators.required]],
      totalBalance: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    if (this.customerId > 0) {
      this.title = 'Edit';
      this._customerService.getCustomerById(this.customerId)
        .subscribe(resp => this.customerForm.setValue(resp)
          , error => this.errorMessage = error);
    }

  }

  save() {

    if (!this.customerForm.valid) {
      return;
    }

    if (this.title === 'Create') {
      this._customerService.saveCustomer(this.customerForm.value)
        .subscribe((data) => {
          this._router.navigate(['/fetch-customer']);
        }, error => this.errorMessage = error);
    }
    if (this.title === 'Edit') {
      this._customerService.updateCustomer(this.customerForm.value)
        .subscribe((data) => {
          this._router.navigate(['/fetch-customer']);
        }, error => this.errorMessage = error);
    }
  }

  cancel() {
    this._router.navigate(['/create-customer']);
  }

  // get name() { return this.customerForm.get('name'); }


}
