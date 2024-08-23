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

        //Constructor
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

        public void AddAccount(Account account)
        {
            accounts.AddAccount(account);
        }

        public bool RemoveAccount(int accountNumber)
        {
            return accounts.RemoveAccount(accountNumber);
        }

        public Account GetAccount(int accountNumber)
        {
            return accounts.FindAccount(accountNumber);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return accounts.GetAllAccounts();
        }

        public override string ToString()
        {
            return $"Welcome, {name}!\n.....Username: {username}\n.....Name: {name}\n.....Address: {address}\n" +
                $"\nAccounts:\n" + accounts.ToString();
        }
    }
}
