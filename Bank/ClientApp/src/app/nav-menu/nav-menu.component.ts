import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../core/services/customer.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  bankName: string;

  collapse() {
    this.isExpanded = false;
  }

  constructor(private customerService: CustomerService) { }

  ngOnInit() {
    this.customerService.getBankName().subscribe((data: string) => {
      this.bankName = data;
    });
  }
  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
