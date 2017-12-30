using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money
{
  abstract  public class Money
    {
        protected int _amount;
        protected string _currency;

        protected Money(int amount, string currency)
        {
            _amount = amount;
            _currency = currency;
        }


        public override bool Equals(object obj)
        {
            Money money = (Money)obj;
            return _amount == money._amount
                && GetType() == money.GetType();
        }


        public static Money dollar(int amount)
        {
            return new Dollar(amount, "USD");
        }

        public static Money franc(int amount)
        {
            return new Franc(amount,"CHF");
        }

        public abstract Money Times(int multiple);

        public  string currency()
        {
            return _currency;
        }
    }
}
