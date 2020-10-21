import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerModel } from '../../models/customer.model';
import { CurrencyModel } from '../../models/currency.model';
import { Account } from '../../models/account.model';
import { CustomerTransactionsHistory } from '../../models/trasactions-history.model';
import { AccountType } from '../../models/account-type.model';
import { AccountTypesDS } from '../../models/account-types-dataset.model';
import { CustomerWithAccounts } from '../../models/customer-with-accounts.model';
import { Bank } from '../../models/bank.model';


@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  constructor(private http: HttpClient) { }

  getCustomersList(): Observable<CustomerModel[]> {
    return this.http.get<CustomerModel[]>('api/customer/GetCustomer');
  }
  getCurrenciesList(): Observable<CurrencyModel[]> {
    return this.http.get<CurrencyModel[]>('api/customer/GetCurrencies');
  }
  getGenAccountNumber(): Observable<string> {
    return this.http.get<string>('api/customer/GetAutoGenAccountNumber');
  }

  getBankName(): Observable<Bank> {
    return this.http.get<Bank>('api/Bank/GetBankName');
  }
  createNewCustomer(tempCustomerWithAccounts: CustomerWithAccounts): Observable<HttpResponse<any>> {
    return this.http.post<HttpResponse<any>>('api/customer/Create', tempCustomerWithAccounts);
  }
  getCustomerAccounts(id: number): Observable<Account[]> {
    return this.http.get<Account[]>('api/Accounts/GetCustomerAccounts/' + id);
  }
  getCustomerTransactionHistory(id: number): Observable<CustomerTransactionsHistory[]> {
    return this.http.get<CustomerTransactionsHistory[]>('api/AccountActionHistory/GetCustomerTransHistory/' + id);
  }
  getAccountTypes(id: number): Observable<AccountType[]> {
    return this.http.get<AccountType[]>('api/AccountTypes/GetAccountTypes/' + id);
  }
  getAccountTypsFromDataSet(): Observable<AccountTypesDS[]> {
    return this.http.get<AccountTypesDS[]>('api/AccountTypes/getAccountsTypsDataSet');
  }

  getEqualsAccount(number): Observable<CustomerModel[]> {
    return this.http.get<CustomerModel[]>('api/customer/getEqualsAccounts/' + number);
  }
}
