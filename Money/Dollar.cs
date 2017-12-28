using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money
{
    public class Dollar : Money
    { 
        public Dollar(int amount)
        {
            _amount = amount;
        }

        public Money Times(int multiple)
        {
            return new Dollar(_amount * multiple);
        }
    }
}
