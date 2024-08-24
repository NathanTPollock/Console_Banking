using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBanking
{
    internal class User
    {
        private string username;
        private string password;
        private string name;
        private string address;
        private AccountList accounts;

        /// <summary>
        /// Creates a new user with a username, password, name, and address.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        public User(string username, string password, string name, string address)
        {
            this.username = username;
            this.password = password;
            this.name = name;
            this.address = address;
            this.accounts = new AccountList();
        }

        // Getters
        public string GetUsername() => username;
        public string GetPassword() => password;
        public string GetName() => name;
        public string GetAddress() => address;

        // Setters
        public void SetUsername(string username) => this.username = username;
        public void SetPassword(string password) => this.password = password;
        public void SetName(string name) => this.name = name;
        public void SetAddress(string address) => this.address = address;

        /// <summary>
        /// Adds an account to the user's account list.
        /// </summary>
        /// <param name="account"></param>
        public void AddAccount(Account account)
        {
            accounts.AddAccount(account);
        }
        /// <summary>
        /// Removes an account from the user's account list.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public bool RemoveAccount(int accountNumber)
        {
            return accounts.RemoveAccount(accountNumber);
        }
        /// <summary>
        /// Retrieves an account from the user's account list.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public Account? GetAccount(int accountNumber)
        {
            return accounts.FindAccount(accountNumber);
        }
        /// <summary>
        /// Retrieves an enumerable of all accounts from the user's account list.
        /// </summary>
        /// <returns>Enumerable of all the user's accounts</returns>
        public IEnumerable<Account> GetAllAccounts()
        {
            return accounts.GetAllAccounts();
        }
        public bool TransferFunds(decimal amount, int accountFrom, int accountTo)
        {
            bool withdrawn = false;
            bool deposited = false;
            try
            {
                GetAccount(accountFrom).WithdrawFunds(amount);
                withdrawn = true;
                GetAccount(accountTo).DepositFunds(amount);
                deposited = true;
            }
            catch(Exception e)
            {
                // If the withdrawal was successful but the deposit was not, deposit the funds back into the original account.
                if (withdrawn && !deposited)
                {
                    GetAccount(accountFrom).DepositFunds(amount);
                    Console.WriteLine("Failure depositing, funds returned.");
                }
                Console.WriteLine("Failure transferring funds - " + e.Message);
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return $"Welcome, {name}!\n.....Username: {username}\n.....Name: {name}\n.....Address: {address}\n" +
                $"\nAccounts:\n" + accounts.ToString();
        }
    }
}
