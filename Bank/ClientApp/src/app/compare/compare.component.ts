import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomerService } from '../core/services/customer.service';
import { CustomerModel } from '../models/customer.model';

@Component({
  selector: 'app-compare',
  templateUrl: './compare.component.html',
  styleUrls: ['./compare.component.css']
})
export class CompareComponent implements OnInit {
  dataSource: any;
  accountNumber: string;
  constructor(private customerService: CustomerService,
    private route: ActivatedRoute) { }

  ngOnInit() {

    this.accountNumber = this.route.snapshot.paramMap.get('number');

    this.customerService.getEqualsAccount(this.accountNumber).subscribe((data: CustomerModel[]) => {
      console.log(data);
      this.dataSource = data;
    });
  }

}
