import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from '../core/services/customer.service';
import { TransactionService } from '../core/services/transaction.service';
import { Account } from '../models/account.model'
import { AccountTransfer } from '../models/accountTransferPost.model';


@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {
  form: FormGroup;


  id: string;
  accountDataSource: MatTableDataSource<Account>;
  displayedAccountsColumns: string[] = ['AccountNumber', 'Currency', 'Balance', 'action'];
  displayedAccountsOperation: string[] = ['transfer', 'deposite', 'withdraw'];


  constructor(private formBuilder: FormBuilder, private transactionService: TransactionService,
    private customerService: CustomerService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.accountDataSource = new MatTableDataSource<Account>([]);
  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    //if (this.id != null)
    //  this.transactionService.doTransaction().subscribe((data: Account) => {
    //    this.CustomerTransactionsHistoryDataSource.data = data;
    //  });

    if (this.id != null) {
      this.customerService.getCustomerAccounts(+this.id).subscribe((accData: Account[]) => {
        console.log(accData);
        debugger
        this.accountDataSource.data = accData;
      });
    }

    this.form = this.formBuilder.group({
      accountFrom: ['', Validators.compose([Validators.required])],
      accountTo: ['', Validators.compose([Validators.required])],
      amount: ['', Validators.compose([Validators.required])],
    })
  }


  onSubmit() {
    debugger
    const tempTransfer: AccountTransfer = new AccountTransfer();


    tempTransfer.accountFrom = this.form.get('accountFrom').value;
    tempTransfer.accountTo = this.form.get('accountTo').value;
    tempTransfer.amount = this.form.get('amount').value;

    this.transactionService.doTransfer(tempTransfer).subscribe((data) => {
      this.router.navigate(['/']);
      console.log(data);
      debugger;
    });
  }

}
