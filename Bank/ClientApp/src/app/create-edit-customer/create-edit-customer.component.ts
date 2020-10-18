import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../core/services/customer.service';
import { CustomerModel } from '../models/customer.model';
import { CurrencyModel } from '../models/currency.model'
import { MatFormField, MatInput, MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';
import { AccountTypesDS } from '../models/account-types-dataset.model';
import { CustomerWithAccounts } from '../models/customer-with-accounts.model';
import { Account } from '../models/account.model';
import { AccountType } from '../models/account-type.model';

@Component({
  selector: 'app-create-edit-customer',
  templateUrl: './create-edit-customer.component.html',
  styleUrls: ['./create-edit-customer.component.css']
})
export class CreateEditCustomerComponent implements OnInit {
  form: FormGroup;
  currencyDataSource: CurrencyModel[];
  accountTypesDS: AccountTypesDS[];


  autoGenNumber: string;
  autoGenSubNumber: string;
  selectedCurrency: string;

  constructor(private formBuilder: FormBuilder,
    private customerService: CustomerService,
    private router: Router,) {

  }


  ngOnInit() {

    this.customerService.getCurrenciesList().subscribe((data: CurrencyModel[]) => {
      this.currencyDataSource = data;
    });

    this.customerService.getAccountTypsFromDataSet().subscribe((data: AccountTypesDS[]) => {
      console.log(data);
      this.accountTypesDS = data;
    });

    this.customerService.getGenAccountNumber().subscribe((data: string) => {
      this.autoGenNumber = data;
    });

    this.customerService.getGenAccountNumber().subscribe((data: string) => {
      this.autoGenSubNumber = data;
    });


    this.form = this.formBuilder.group({
      name: ['', Validators.compose([Validators.required])],
      mainCurrency: ['', Validators.compose([Validators.required])],
      MainAccountNumber: ['', Validators.compose([Validators.required])],
      totalBalance: ['', Validators.compose([Validators.required])],

      subAccountNumber: ['', Validators.compose([Validators.required])],
      balance: ['', Validators.compose([Validators.required])],
      currency: ['', Validators.compose([Validators.required])],

      accountType: ['', Validators.compose([Validators.required])]
    })
  }


  onSubmit() {
    debugger
    const tempCustomerWithAccounts: CustomerWithAccounts = new CustomerWithAccounts();

    tempCustomerWithAccounts.customer = new CustomerModel();
    tempCustomerWithAccounts.customer.CustomerName = this.form.get('name').value;
    tempCustomerWithAccounts.customer.MainCurrency = this.form.get('mainCurrency').value;
    tempCustomerWithAccounts.customer.MainAccountNumber = this.form.get('MainAccountNumber').value;
    tempCustomerWithAccounts.customer.TotalBalance = this.form.get('totalBalance').value;

    tempCustomerWithAccounts.account = new Account();
    tempCustomerWithAccounts.account.AccountNumber = this.form.get('subAccountNumber').value;
    tempCustomerWithAccounts.account.Balance = this.form.get('balance').value;
    tempCustomerWithAccounts.account.Currency = this.form.get('currency').value;

    tempCustomerWithAccounts.accountType = new AccountType();
    tempCustomerWithAccounts.accountType.AccountType = this.form.get('accountType').value;


    this.customerService.createNewCustomer(tempCustomerWithAccounts).subscribe((data) => {
      this.router.navigate(['/']);
      console.log(data);
      debugger;
    });
  }
}
