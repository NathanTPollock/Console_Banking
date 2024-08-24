using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBanking
{
    internal interface IAccount
    {
        int GetAccountNumber();
        decimal GetBalance();
        string GetOpenedDate();
        decimal GetAccountFee();
        string GetAccountType();
        string GetAccountStatus();
        decimal DepositFunds(decimal amount);
        decimal WithdrawFunds(decimal amount);
        decimal ChargeFee();
        string ToString();
    }
}
