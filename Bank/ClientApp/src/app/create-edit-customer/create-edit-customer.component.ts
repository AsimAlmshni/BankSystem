import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../core/services/customer.service';
import { CustomerModel } from '../models/customer.model';

@Component({
  selector: 'app-create-edit-customer',
  templateUrl: './create-edit-customer.component.html',
  styleUrls: ['./create-edit-customer.component.css']
})
export class CreateEditCustomerComponent implements OnInit {
  form: FormGroup;
  constructor(private formBuilder: FormBuilder,
    private customerService: CustomerService) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      name: ['', Validators.compose([Validators.required])],
      mainCurrency: ['', Validators.compose([Validators.required])],
      accId: ['', Validators.compose([Validators.required])],
      totalBalance: ['', Validators.compose([Validators.required])]
    })
  }

  onSubmit() {
    const tempCustomer: CustomerModel = new CustomerModel();
    tempCustomer.name = this.form.get('name').value;
    tempCustomer.mainCurrency = this.form.get('mainCurrency').value;
    tempCustomer.accId = this.form.get('accId').value;
    tempCustomer.totalBalance = this.form.get('totalBalance').value;
    this.customerService.createNewCustomer(tempCustomer).subscribe((data) => {
      console.log(data);
      debugger;
    });
  }
}