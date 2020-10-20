import { AccountType } from "./account-type.model";
import { CustomerModel } from "./customer.model";
import { Account } from "./account.model";

export class CustomerWithAccounts {
  customer: CustomerModel;
  accounts: Account[] = [];
  accountTypes: AccountType[] = [];
}
