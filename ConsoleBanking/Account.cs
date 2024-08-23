using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBanking
{
    internal abstract class Account
    {
        // Due to background interest thread, we need to lock our balance methods.
        private readonly object balanceLock = new object();

        private int accountNumber;
        private decimal balance;
        private string openedDate;
        protected decimal accountFee;
        public enum AccountType { Checking, Savings }
        protected AccountType accountType;
        public enum AccountStatus { New, Active, Frozen, Closed}
        private AccountStatus status;
        private static int nextAccountNumber = 1;
        
        // Constructor
        public Account()
        {
            SetAccountNumber();
            balance = 100;
            openedDate = DateTime.Now.ToString("MM/dd/yyyy");
            status = AccountStatus.New;
        }
        // Getters
        public int GetAccountNumber() => accountNumber;
        public decimal GetBalance()
        {
           lock (balanceLock)
            {
                return balance;
            }
        }
        public string GetOpenedDate() => openedDate;
        public decimal GetAccountFee() => accountFee;
        public string GetAccountType() => accountType.ToString();
        public string GetAccountStatus() => status.ToString();

        // Setters
        private void SetAccountNumber()
        {
            accountNumber = nextAccountNumber;
            nextAccountNumber++;
        }
        /// <summary>
        /// Deposits funds into the account.
        /// </summary>
        /// <param name="amount">Amount to deposit</param>
        /// <returns>New balance</returns>
        /// <exception cref="ArgumentException">Amount was non-positive</exception>
        public decimal DepositFunds(decimal amount)
        {
            lock (balanceLock)
            {
                if (amount <= 0) throw new ArgumentException("Amount must be greater than 0.");
                balance += amount;
                return balance;
            }
        }
        /// <summary>
        /// Withdraws funds from the account.
        /// </summary>
        /// <param name="amount">Amount to withdraw from the account</param>
        /// <returns>New balance</returns>
        /// <exception cref="ArgumentException">
        /// Invalid amount (non-positive or insufficient funds)
        /// </exception>
        public decimal WithdrawFunds(decimal amount)
        {
            lock (balanceLock)
            {
            if (amount <= 0) throw new ArgumentException("Amount must be greater than 0.");
            if (amount > balance) throw new ArgumentException("Insufficient funds.");
            balance -= amount;
            return balance;
            }

        }

        /// <summary>
        /// Account maintenence fee charged, regardless of if it makes account negative.
        /// </summary>
        /// <returns></returns>
        public decimal ChargeFee()
        {
            balance = balance - accountFee;
            return balance;
        }

        public abstract void SetAccountFee(decimal fee);

        // public void SetBalance(decimal balance) => this.balance = balance;

        public override string ToString()
        {
            return $"Account #{accountNumber}\n" +
                $".....Balance: {balance:C}\n" +
                $".....Opened: {openedDate}\n" +
                $".....Fee: {accountFee}\n" +
                $".....Type: {accountType.ToString()}\n" +
                $".....Status: {status.ToString()}\n";
        }
    }
}
