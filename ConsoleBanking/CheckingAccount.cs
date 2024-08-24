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
        public CheckingAccount() : base()
        {
            accountType = AccountType.Checking;
            accountFee = 0;
        }
        /// <summary>
        /// Sets account fee with a minimum of 0.
        /// </summary>
        /// <param name="fee"></param>
        /// <exception cref="ArgumentException"></exception>
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
