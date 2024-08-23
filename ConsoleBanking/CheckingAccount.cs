using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBanking
{
    internal class CheckingAccount : Account
    {
        //private DebitCard card;

        public CheckingAccount() : base()
        {
            accountType = AccountType.Checking;
            accountFee = 0;
        }

        public override void SetAccountFee(decimal fee = 0)
        {
            if (fee < 0) throw new ArgumentException("Fee is less than minimum of 0");
            accountFee = fee;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
