import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../core/services/customer.service';
import { CustomerModel } from '../models/customer.model';
import { CurrencyModel } from '../models/currency.model'
import { MatFormField, MatInput, MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-create-edit-customer',
  templateUrl: './create-edit-customer.component.html',
  styleUrls: ['./create-edit-customer.component.css']
})
export class CreateEditCustomerComponent implements OnInit {
  form: FormGroup;
  currencyDataSource: CurrencyModel[];// = new MatTableDataSource(ELEMENT_DATA);
  autoGenNumber: string;
  selectedCurrency: string;

  constructor(private formBuilder: FormBuilder,
    private customerService: CustomerService) {

  }


  ngOnInit() {

    this.customerService.getCurrenciesList().subscribe((data: CurrencyModel[]) => {

      this.currencyDataSource = data;
    });

    this.customerService.getGenAccountNumber().subscribe((data: string) => {

      this.autoGenNumber = data;
    });

    this.form = this.formBuilder.group({
      name: ['', Validators.compose([Validators.required])],
      mainCurrency: ['', Validators.compose([Validators.required])],
      accId: ['', Validators.compose([Validators.required])],
      totalBalance: ['', Validators.compose([Validators.required])]
    })
  }

  selectChangeHandler(event: any) {
    //update the ui
    debugger
    this.selectedCurrency = event.target.value;
  }

  onSubmit() {
    debugger
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
