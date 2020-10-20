import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
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
  accountForm: Account;

  //accountDataForm: Account;
  //accountTypesDataForm: AccountType;

  autoGenNumber: string;
  autoGenSubNumber: string;
  selectedCurrency: string;


  constructor(private formBuilder: FormBuilder,
    private customerService: CustomerService,
    private router: Router,) {

  }

  get moreForms() {
    return this.formBuilder.group({
      //subAccountNumber: "",
      currency: "",
      balance: "",
      accountTypes: ""
    });
  }

  addMoreForms() {
    (this.form.get("moreForms") as FormArray).push(this.moreForms);
  }

  deleteForm(index) {
    (this.form.get("moreForms") as FormArray).removeAt(index);
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

      //subAccountNumber: ['', Validators.compose([Validators.required])],
      //balance: ['', Validators.compose([Validators.required])],
      //currency: ['', Validators.compose([Validators.required])],

      //accountType: ['', Validators.compose([Validators.required])]
      moreForms: this.formBuilder.array([this.moreForms])
    })
  }
  


  onSubmit() {
    const tempCustomerWithAccounts: CustomerWithAccounts = new CustomerWithAccounts();

    tempCustomerWithAccounts.customer = new CustomerModel();

    tempCustomerWithAccounts.customer.CustomerName = this.form.get('name').value;
    tempCustomerWithAccounts.customer.MainCurrency = this.form.get('mainCurrency').value;
    tempCustomerWithAccounts.customer.MainAccountNumber = this.form.get('MainAccountNumber').value;
    tempCustomerWithAccounts.customer.TotalBalance = this.form.get('totalBalance').value;

    var addAccountFormData = this.form.get('moreForms').value;

    debugger 


    for (let i = 0; i < addAccountFormData.length; i++) {
      var accountDataForm = new Account();
      var accountTypesDataForm = new AccountType();

      accountDataForm.Balance = addAccountFormData[i].balance;
      accountDataForm.Currency = addAccountFormData[i].currency;

      accountTypesDataForm = addAccountFormData[i].accountTypes;

      tempCustomerWithAccounts.accounts.push(accountDataForm);
      tempCustomerWithAccounts.accountTypes.push(accountTypesDataForm);
    }

    console.log(tempCustomerWithAccounts);
    //tempCustomerWithAccounts.accounts.AccountNumber = this.form.get('subAccountNumber').value;
    //tempCustomerWithAccounts.accounts.Balance = this.form.get('balance').value;
    //tempCustomerWithAccounts.accounts.Currency = this.form.get('currency').value;

    //tempCustomerWithAccounts.accountTypes = this.form.get('accountType').value;


    this.customerService.createNewCustomer(tempCustomerWithAccounts).subscribe((data) => {
      this.router.navigate(['/']);
      console.log(data);
      debugger;
    });
    debugger

  }
}
