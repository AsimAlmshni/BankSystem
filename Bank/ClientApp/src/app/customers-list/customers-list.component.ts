import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { CustomerService } from '../core/services/customer.service';
import { CustomerModel } from '../models/customer.model';

@Component({
  selector: 'app-customers-list',
  templateUrl: './customers-list.component.html',
  styleUrls: ['./customers-list.component.css']
})
export class CustomersListComponent implements OnInit {
  displayedColumns: string[] = ['CustomerName', 'mainCurrency', 'MainAccountNumber', 'totalBalance'];
  dataSource: MatTableDataSource<CustomerModel>;// = new MatTableDataSource(ELEMENT_DATA);
  bankName: string;

  constructor(private customerService: CustomerService) { 
    this.dataSource = new MatTableDataSource<CustomerModel>([]);
  }

  ngOnInit() {
    
    this.customerService.getCustomersList().subscribe((data: CustomerModel[]) => {

      this.dataSource.data = data;
      debugger
    });


  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
