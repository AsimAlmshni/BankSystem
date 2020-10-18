import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../core/services/customer.service';
import { Bank } from '../models/bank.model';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  bank: Bank;

  collapse() {
    this.isExpanded = false;
  }

  constructor(private customerService: CustomerService) { }

  ngOnInit() {
    this.customerService.getBankName().subscribe((data: Bank) => {
      debugger
      this.bank = data;
    });
  }
  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
