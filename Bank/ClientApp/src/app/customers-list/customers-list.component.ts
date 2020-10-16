import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { CustomerService } from '../core/services/customer.service';
import { CustomerModel } from '../models/customer.model';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Account } from '../models/account.model'



@Component({
  selector: 'app-customers-list',
  templateUrl: './customers-list.component.html',
  styleUrls: ['./customers-list.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0', display: 'none' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class CustomersListComponent implements OnInit {
  displayedColumns: string[] = ['CustomerID', 'CustomerName', 'mainCurrency', 'MainAccountNumber', 'totalBalance'];
  displayedAccountsColumns: string[] = ['AccId', 'AccountNumber', 'Currency', 'Balance'];




  dataSource: MatTableDataSource<CustomerModel>;
  bankName: string;
  CustomerAccountsSource: MatTableDataSource<Account>;


  constructor(private customerService: CustomerService) { 
    this.dataSource = new MatTableDataSource<CustomerModel>([]);
  }

  ngOnInit() {
    this.customerService.getCustomersList().subscribe((data: CustomerModel[]) => {
      this.dataSource.data = data;
      //debugger
    });


    this.customerService.getBankName().subscribe((data: string) => {
      this.bankName = data;
    });


  }

  getAccounts(id: number) {
    this.customerService.getCustomerAccounts(id).subscribe((accData: any) => {
      console.log(accData);
      this.CustomerAccountsSource.data = accData;
      debugger
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
