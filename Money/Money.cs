using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money
{
    public class Money : Expression
    {
        public int _amount;
        protected string _currency;

        public Money(int amount, string currency)
        {
            _amount = amount;
            _currency = currency;
        }


        public override bool Equals(object obj)
        {
            Money money = (Money)obj;
            return _amount == money._amount
                && currency().Equals(money.currency());
        }


        public static Money dollar(int amount)
        {
            return new Money(amount, "USD");
        }

        public static Money franc(int amount)
        {
            return new Money(amount,"CHF");
        }

        public Money Times(int multiple)
        {
            return new Money(_amount * multiple, _currency);
        }

        public  string currency()
        {
            return _currency;
        }

        public String toString()
        {
            return _amount + " " + _currency;
        }

        public Expression plus(Money addend)
        {
            return new Sum(this, addend);
        }

        Money reduce(string to)
        {
            return this;
        }
    }

    public class Sum : Expression
    {
        public Money augend;
        public Money addend;

        public Sum(Money augend, Money addend)
        {
            this.augend = augend;
            this.addend = addend;
        }

        public Money reduce(string to)
        {
            int amount = augend._amount + addend._amount;
            return new Money(amount,to);
        }
    }

    public class Bank
    {
        public Money reduce(Expression source, string to)
        {
            return source.reduce(to);
        }
    }


    public interface Expression
    {
        Money reduce(string to);
    }
}
