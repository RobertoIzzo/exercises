using System;
using System.Collections;
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

        public Money reduce(Bank bank, string to)
        {
            int rate = bank.rate(_currency, to);
            return new Money(_amount /rate,to);
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

        public Money reduce(Bank bank, string to)
        {
            int amount = augend._amount + addend._amount;
            return new Money(amount,to);
        }
    }

    public class Bank
    {
        private Hashtable rates = new Hashtable();

        public int rate(string from , string to)
        {
            if (from.Equals(to)) return 1;
          return (int) rates[new Pair(from, to)];
        }
        
        public Money reduce(Expression source, string to)
        {
            return source.reduce(this,to);
        }

        public void addRate(string from, string to, int rate)
        {
            rates.Add(new Pair(from,to),rate );
        }
    }
    public class Pair
    {
        private string from;
        private string to;

        public Pair(string from, string to)
        {
            this.from = from;
            this.to = to;
        }

        public bool equals(Object obj)
        {
            Pair pair = (Pair)obj;
            return from.Equals(pair.from) && to.Equals(pair.to);
        }

        public int hashCode()
        {
            return 0;
        }
    }

    public interface Expression
    {
        Money reduce(Bank bank, string to);
    }
}
