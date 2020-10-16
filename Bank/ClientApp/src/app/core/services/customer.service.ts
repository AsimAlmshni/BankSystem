import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerModel } from '../../models/customer.model';
import { CurrencyModel } from '../../models/currency.model';

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

  getBankName(): Observable<string> {
    return this.http.get<string>('api/Bank/GetBankName');
  }

  createNewCustomer(customer: CustomerModel): Observable<HttpResponse<any>> {
    return this.http.post<HttpResponse<any>>('api/customer/Create', customer);
  }
}
