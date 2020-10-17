import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { CustomerService } from '../core/services/customer.service';
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



  constructor(private customerService: CustomerService, private route: ActivatedRoute) {
    this.CustomerTransactionsHistoryDataSource = new MatTableDataSource<CustomerTransactionsHistory>([]);

  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.customerService.getCustomerTransactionHistory(+this.id).subscribe((data: CustomerTransactionsHistory[]) => {
      debugger
      console.log(data);
      this.CustomerTransactionsHistoryDataSource.data = data;
    });
  }


}
