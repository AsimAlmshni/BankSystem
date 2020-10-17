import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { CustomerService } from '../core/services/customer.service';
import { TransactionService } from '../core/services/transaction.service';
import { CustomerTransactionsHistory } from '../models/trasactions-history.model';

@Component({
  selector: 'app-account-transaction-history',
  templateUrl: './account-transaction-history.component.html',
  styleUrls: ['./account-transaction-history.component.css']
})
export class AccountTransactionHistoryComponent implements OnInit {

  id: string;

  CustomerTransactionsHistoryDataSource: MatTableDataSource<CustomerTransactionsHistory>;
  displayedColumnsAccTransHistory: string[] = ['aahid', 'amount', 'date', 'actionType', 'fromAccount', 'toAccount', 'currency'];



  constructor(private customerService: CustomerService, private route: ActivatedRoute, private transactionService: TransactionService) {
    this.CustomerTransactionsHistoryDataSource = new MatTableDataSource<CustomerTransactionsHistory>([]);

  }

  ngOnInit() {
    debugger
    this.id = this.route.snapshot.paramMap.get('id');
    if(this.id != null)
      this.customerService.getCustomerTransactionHistory(+this.id).subscribe((data: CustomerTransactionsHistory[]) => {
        this.CustomerTransactionsHistoryDataSource.data = data;
      });

    this.transactionService.getTransactions().subscribe((data: CustomerTransactionsHistory[]) => {
      this.CustomerTransactionsHistoryDataSource.data = data;
    });
  }


}
