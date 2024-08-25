# Welcome to Console Bank
This application builds out the logic for a banking application. In the future, a separate app with a GUI will be created based off of this app's logic.

-----------------

# Design Document
## Class Summaries
#### *Class User*
Stores the customer's information including their username, password, name, address, and a list of their accounts. 
#### *Class AccountList*
Contains a dictionary of the user's accounts, using the account number as the key.
#### *Interface IAccount*
Defines the behavior that the account class must have including deposit, withdrawal, and information request methods.
#### *Abstract Class Account*
Implements IAccount and enforces the deposit, withdrawal, and information request behaviors. Additionally defines an abstract method to set the account's fee.
#### *Class SavingsAccount*
Inherits abstract class account and defines SetAccountFee while ensuring that the fee is not set below the minimum of $10. Also defines and implements a method to compound interest
#### *Class CheckingAccount*
Inherits abstract class account and defines SetAccoutFee while ensuring that the fee is not set below the minimum of $0.

## *Class Diagram*
![Banking Application Class Diagram (3)](https://github.com/user-attachments/assets/4967d347-6666-4eae-ab7e-fbaffc8f9e92)

## *Activity Diagram*
![Banking Application Activity Diagram (2)](https://github.com/user-attachments/assets/46caab46-3650-436a-b57a-e91d7edd0882)

--------------
# Notes
## Current Release Features
- Design Document
- Create User
- User Login
- Create Account
- Access Account
- Transfer Between Accounts
- Background Compound Interest
- Background Charge Account Fees
## Future Features
- Store/Load data
- Encrypt data
