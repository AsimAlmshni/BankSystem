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


  onSubmit(formName: string, accountNo: string) {
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

        if (this.validateAmountTransfered(accountNo, tempTransfer.Amount) >= 0)
          this.transactionService.doTransfer(tempTransfer).subscribe((data) => {
            this.router.navigate(['/']);
          });
        else
        alert("the transaction couldnot be done trans amount > balance");

      }
    } else if (formName === 'deposite') {
      const tempDeposite: AccountTransfer = new AccountTransfer();
      tempDeposite.FromAccount = this.depositeForm.get('account').value;
      tempDeposite.Amount = this.depositeForm.get('amount').value;

      var count1 = this.depositeForm.get('currency').value;
      var count2 = this.depositeForm.get('balance').value;

      this.transactionService.doDeposite(tempDeposite).subscribe((data) => {
        this.router.navigate(['/']);
      });
    } else if (formName === 'withdraw') {
      const tempWithdraw: AccountTransfer = new AccountTransfer();

      tempWithdraw.FromAccount = this.WithdrawForm.get('accountWithdraw').value;
      tempWithdraw.Amount = this.WithdrawForm.get('amountWithdraw').value;

      var count3 = this.WithdrawForm.get('currencyWithdraw').value;
      var count4 = this.WithdrawForm.get('balanceWithdraw').value;
      if (this.validateAmountTransfered(accountNo, tempWithdraw.Amount) >= 0)
        this.transactionService.doWithdraw(tempWithdraw).subscribe((data) => {
          this.router.navigate(['/']);
        });
      else
      alert("the transaction couldnot be done trans amount > balance");
    }

  }

  validateAmountTransfered(accountNo: string, amount: number) {
    var temp = 0;
    debugger
    var accountData = JSON.parse(JSON.stringify(this.accountDataSource.data));
    accountData.forEach((data) => {
      if (data.accountNumber == accountNo) {
        temp = data.balance;
      }
    });
    return temp - amount;
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
