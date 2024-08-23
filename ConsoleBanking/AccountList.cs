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
        public Account FindAccount(int accountNumber)
        {
            if (accounts.TryGetValue(accountNumber, out Account account)) return account;
            return null;
        }

        public bool RemoveAccount(int accountNumber)
        {
            return accounts.Remove(accountNumber);
        }

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
