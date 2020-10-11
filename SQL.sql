CREATE TABLE AccountActionHistory (AAHID int IDENTITY NOT NULL, Amount float(53) NOT NULL, [Date] date NOT NULL, ActionType varchar(10) NOT NULL, FromAccount varchar(255) NOT NULL, ToAccount varchar(255) NOT NULL, Currency varchar(10) NOT NULL, AccountsAccID int NOT NULL, PRIMARY KEY (AAHID));
CREATE TABLE Accounts (AccID int IDENTITY NOT NULL, AccountNumber varchar(25) NOT NULL UNIQUE, Currency varchar(10) NOT NULL, CustomerID int NOT NULL, Balance float(53) NOT NULL, PRIMARY KEY (AccID));
CREATE TABLE AccountTypes (AccTypID int IDENTITY NOT NULL, AccountType varchar(10) NOT NULL, AccIDTyp int NOT NULL, PRIMARY KEY (AccTypID));
CREATE TABLE Bank (BankID int IDENTITY NOT NULL, BankName varchar(255) NOT NULL, PRIMARY KEY (BankID));
CREATE TABLE Client (CliID int IDENTITY NOT NULL, BankID int NOT NULL, PRIMARY KEY (CliID));
CREATE TABLE Customer (CustomerID int IDENTITY NOT NULL, MainAccountNumber varchar(255) NOT NULL UNIQUE, MainCurrency varchar(10) NOT NULL, TotalBalance float(53) NOT NULL, CustomerName varchar(255) NOT NULL, ClientID int NOT NULL, PRIMARY KEY (CustomerID));
ALTER TABLE AccountActionHistory ADD CONSTRAINT FKAccountAct322473 FOREIGN KEY (AccountsAccID) REFERENCES Accounts (AccID);
ALTER TABLE AccountTypes ADD CONSTRAINT [account have many types] FOREIGN KEY (AccIDTyp) REFERENCES Accounts (AccID);
ALTER TABLE Client ADD CONSTRAINT [Bank has] FOREIGN KEY (BankID) REFERENCES Bank (BankID);
ALTER TABLE Customer ADD CONSTRAINT [client serve more than one customer ] FOREIGN KEY (ClientID) REFERENCES Client (CliID);
ALTER TABLE Accounts ADD CONSTRAINT [customer could have many accounts] FOREIGN KEY (CustomerID) REFERENCES Customer (CustomerID);
