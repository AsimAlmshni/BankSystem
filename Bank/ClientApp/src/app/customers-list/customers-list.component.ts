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
  displayedColumns: string[] = ['CustomerName', 'mainCurrency', 'MainAccountNumber', 'totalBalance'];
  displayedAccountsColumns: string[] = ['AccountNumber', 'Currency', 'Balance', 'action'];




  bankName: string;
  CustomerAccountsSource: MatTableDataSource<Account>;
  dataSource: MatTableDataSource<CustomerModel>;


  constructor(private customerService: CustomerService) { 
    this.dataSource = new MatTableDataSource<CustomerModel>([]);
    this.CustomerAccountsSource = new MatTableDataSource<Account>([]);



  }

  ngOnInit() {
    this.customerService.getCustomersList().subscribe((data: CustomerModel[]) => {
      console.log(data);
      this.dataSource.data = data;
      //debugger
    });


    this.customerService.getBankName().subscribe((data: string) => {
      debugger
      this.bankName = data;
    });


  }

  getAccounts(id: number, a: boolean) {
    if(a === true)
      this.customerService.getCustomerAccounts(id).subscribe((accData: Account[]) => {
        console.log(accData);
        this.CustomerAccountsSource.data = accData;
        //debugger
      });
  }

  getCustomerTransHistory(id: number) {
    location.href = "/transHistory/view/" + id;

  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
