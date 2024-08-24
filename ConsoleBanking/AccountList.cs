using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBanking
{
    internal class AccountList
    {
        private Dictionary<int, Account> accounts = new Dictionary<int, Account>();

        /// <summary>
        /// Stores an account in the account list.
        /// </summary>
        /// <param name="account"></param>
        /// <returns>False if account is null, true if account was successfully added</returns>
        public bool AddAccount(Account account)
        {
            if (account == null) return false;
            accounts.Add(account.GetAccountNumber(), account);
            return true;
        }
        /// <summary>
        /// Finds account in the account list.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns>Account if account is found. Null if not found</returns>
        public Account? FindAccount(int accountNumber)
        {
            if (accounts.TryGetValue(accountNumber, out Account? account)) return account;
            return null;
        }
        /// <summary>
        /// Removes account from the account list.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns>True if found and removed. False otherwise</returns>
        public bool RemoveAccount(int accountNumber)
        {
            return accounts.Remove(accountNumber);
        }
        /// <summary>
        /// Retrieves the number of accounts in this account list.
        /// </summary>
        /// <returns>Count of account in the account list dictionary.</returns>
        public int Size()
        {
            return accounts.Count;
        }
        /// <summary>
        /// Retrieves all accounts in this account list.
        /// </summary>
        /// <returns>Values of account list in an enumberable.</returns>
        public IEnumerable<Account> GetAllAccounts()
        {
            return accounts.Values;
        }

        public override string ToString()
        {
            if (accounts.Count == 0) return "No accounts found.";
            StringBuilder sb = new StringBuilder();
            foreach (Account account in accounts.Values)
            {
                sb.Append(account.ToString() + "\n");
            }
            return sb.ToString();
        }
    }
}
