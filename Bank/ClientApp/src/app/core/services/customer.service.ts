import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerModel } from 'src/app/models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  constructor(private http: HttpClient) { }

  getCustomersList(): Observable<CustomerModel[]> {
    return this.http.get<CustomerModel[]>('api/customers');
  }
  createNewCustomer(customer: CustomerModel): Observable<HttpResponse<any>> {
    return this.http.post<HttpResponse<any>>('api/customer/create', customer);
  }
}