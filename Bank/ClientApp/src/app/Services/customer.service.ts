import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
//import './rxjs-operators';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';




@Injectable()
export class CustomerService {
  myAppUrl: string = "";

  constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

 
  getCustomer() {
    return this._http.get(this.myAppUrl + 'api/Employee/Index')
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }

  getCustomerById(id: number) {
    return this._http.get(this.myAppUrl + "api/Customer/Details/" + id)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }

  saveCustomer(employee) {
    return this._http.post(this.myAppUrl + 'api/Customer/Create', employee)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }

  updateCustomer(employee) {
    return this._http.put(this.myAppUrl + 'api/Customer/Edit', employee)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }


  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }
}
