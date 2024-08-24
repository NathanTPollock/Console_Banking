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
        /// <summary>
        /// Utilizes deposit method to add interest to the account according to set interestRate.
        /// </summary>
        /// <returns>Amount of interest added</returns>
        public decimal CompoundInterest()
        {
            if (GetBalance() < 0) return 0.001m;
            decimal interest = (decimal)interestRate * GetBalance();
            DepositFunds(interest);
            return interest;
        }
        /// <summary>
        /// Sets account fee with a minimum of 10.
        /// </summary>
        /// <param name="fee"></param>
        /// <exception cref="ArgumentException"></exception>
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
