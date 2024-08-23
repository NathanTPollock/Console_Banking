using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBanking
{
    internal class SavingsAccount : Account
    {
        private double interestRate;

        public SavingsAccount() : base()
        {
            accountType = AccountType.Savings;
            accountFee = 10;
            interestRate = 0.04;
        }

        public decimal CompoundInterest()
        {
            decimal interest = (decimal)interestRate * GetBalance();
            DepositFunds(interest);
            return interest;
        }

        public override void SetAccountFee(decimal fee = 10)
        {
            if (fee < 10) throw new ArgumentException("Fee is less than minimum of 10");
            accountFee = fee;
        }
        public override string ToString()
        {
            return base.ToString() +
                $".....Interest Rate: {interestRate}\n";
        }
    }
}
