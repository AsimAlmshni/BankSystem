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
  depositeForm: FormGroup;
  WithdrawForm: FormGroup;

  id: string;
  accountDataSource: MatTableDataSource<Account>;
  filteredAccountDS: Account;
  filteredAccountWithdrawDS: Account;
  filteredAccountTransferDS: Account;
  filteredAccountTransferAcc2DS: Account;

  displayedAccountsColumns: string[] = ['AccountNumber', 'Currency', 'Balance', 'action'];
  displayedAccountsOperation: string[] = ['transfer', 'deposite', 'withdraw'];


  constructor(private withdrawFormBuilder: FormBuilder, private depositFormBuilder: FormBuilder, private transferFormBuilder: FormBuilder, private transactionService: TransactionService,
    private customerService: CustomerService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.accountDataSource = new MatTableDataSource<Account>([]);
    this.filteredAccountDS = new Account();
    this.filteredAccountWithdrawDS = new Account();
    this.filteredAccountTransferDS = new Account();
    this.filteredAccountTransferAcc2DS = new Account();
  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    //if (this.id != null)
    //  this.transactionService.doTransaction().subscribe((data: Account) => {
    //    this.CustomerTransactionsHistoryDataSource.data = data;
    //  });

    if (this.id != null) {
      this.customerService.getCustomerAccounts(+this.id).subscribe((accData: Account[]) => {
        this.accountDataSource.data = accData;
      });
    }

    this.form = this.transferFormBuilder.group({
      accountFrom: ['', Validators.compose([Validators.required])],
      balanceAcc1: ['', Validators.compose([Validators.required])],
      currencyAcc1: ['', Validators.compose([Validators.required])],
      accountTo: ['', Validators.compose([Validators.required])],
      currencyAcc2: ['', Validators.compose([Validators.required])],
      amount: ['', Validators.compose([Validators.required])],
    })

    this.depositeForm = this.depositFormBuilder.group({
      account: ['', Validators.compose([Validators.required])],
      currency: ['', Validators.compose([Validators.required])],
      amount: ['', Validators.compose([Validators.required])],
      balance: ['', Validators.compose([Validators.required])],
    });

    this.WithdrawForm = this.withdrawFormBuilder.group({
      accountWithdraw: ['', Validators.compose([Validators.required])],
      currencyWithdraw: ['', Validators.compose([Validators.required])],
      amountWithdraw: ['', Validators.compose([Validators.required])],
      balanceWithdraw: ['', Validators.compose([Validators.required])],
    });
  }


  onSubmit(formName: string) {
    debugger
    if (formName === 'transfer') {
      const tempTransfer: AccountTransfer = new AccountTransfer();

      tempTransfer.FromAccount = this.form.get('accountFrom').value;
      tempTransfer.ToAccount = this.form.get('accountTo').value;
      tempTransfer.Amount = this.form.get('amount').value;

      var blnc = this.form.get('balanceAcc1').value;
      var curren1 = this.form.get('currencyAcc1').value;
      var curren2 = this.form.get('currencyAcc2').value;

      if (curren1 === curren2) {
        this.transactionService.doTransfer(tempTransfer).subscribe((data) => {
          this.router.navigate(['/']);
        });
      } else {
        alert("there would be an exchange because currencies not equals");
        this.transactionService.doTransfer(tempTransfer).subscribe((data) => {
          this.router.navigate(['/']);
        });
      }
    } else if (formName === 'deposite') {
      const tempDeposite: Account = new Account();


      tempDeposite.AccountNumber = this.form.get('account').value;
      tempDeposite.Currency = this.form.get('currency').value;
      tempDeposite.Balance = this.form.get('amount').value;


      this.transactionService.doDeposite(tempDeposite).subscribe((data) => {
        this.router.navigate(['/']);
      });
    } else if (formName === 'withdraw') {
      const tempWithdraw: Account = new Account();


      tempWithdraw.AccountNumber = this.form.get('accountWithdraw').value;
      tempWithdraw.Currency = this.form.get('currencyWithdraw').value;
      tempWithdraw.Balance = this.form.get('amountWithdraw').value;

      this.transactionService.doWithdraw(tempWithdraw).subscribe((data) => {
        this.router.navigate(['/']);
      });
    }

  }

  filterAccount(accountNo: string) {

    var accountData = JSON.parse(JSON.stringify(this.accountDataSource.data));

    accountData.forEach((data) => {
      if (data.accountNumber == accountNo) {
        this.filteredAccountDS.Balance = data.balance;
        this.filteredAccountDS.Currency = data.currency;
      }
    });
  }

  filterAccountWithdraw(accountNo: string) {

    var accountData = JSON.parse(JSON.stringify(this.accountDataSource.data));

    accountData.forEach((data) => {
      if (data.accountNumber == accountNo) {
        this.filteredAccountWithdrawDS.Balance = data.balance;
        this.filteredAccountWithdrawDS.Currency = data.currency;
      }
    });
  }

  filterAccountTransfer(accountNo: string) {

    var accountData = JSON.parse(JSON.stringify(this.accountDataSource.data));

    accountData.forEach((data) => {
      if (data.accountNumber == accountNo) {
        this.filteredAccountTransferDS.Balance = data.balance;
        this.filteredAccountTransferDS.Currency = data.currency;
      }
    });
  }

  filterAccountTransferAcc2(accountNo: string) {

    var accountData = JSON.parse(JSON.stringify(this.accountDataSource.data));

    accountData.forEach((data) => {
      if (data.accountNumber == accountNo) {
        this.filteredAccountTransferAcc2DS.Balance = data.balance;
        this.filteredAccountTransferAcc2DS.Currency = data.currency;
      }
    });
  }
}
