using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money
{
    public class Franc :Money
    {

        public Franc(int amount,string currency) 
            : base(amount, currency)
        {
            _amount = amount;
            _currency = "CHF";
        }

        public override Money Times(int multiple)
        {
            return Money.franc(_amount * multiple);
        }
    }
}
