using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money
{
    public class Franc :Money
    {
        public Franc(int amount)
        {
            _amount = amount;
        }

      
        public Money Times(int multiple)
        {
            return new Franc(_amount * multiple);
        }
    }
}
