import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerModel } from '../../models/customer.model';
import { CurrencyModel } from '../../models/currency.model';
import { Account } from '../../models/account.model';
import { CustomerTransactionsHistory } from '../../models/trasactions-history.model';
import { AccountTransfer } from '../../models/accountTransferPost.model';


@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  constructor(private http: HttpClient) { }

  getTransactions(): Observable<CustomerTransactionsHistory[]> {
    return this.http.get<CustomerTransactionsHistory[]>('api/AccountActionHistory/GetTransactions');
  }

  doTransfer(tempAccount: AccountTransfer): Observable<HttpResponse<any>> {
    return this.http.post<HttpResponse<any>>('api/Client/doTransfer', tempAccount);
  }
}
