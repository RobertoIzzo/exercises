using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money
{
    public class Dollar : Money
    {
        public Dollar(int amount, string currency) 
            : base(amount, currency)
        {
            _amount = amount;
            _currency = "USD";
        }

        public override Money Times(int multiple)
        {
            return Money.dollar(_amount * multiple);
        }
    }
}
